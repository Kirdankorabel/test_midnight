using Controller.Building;
using Model;
using System.Collections.Generic;
using UnityEngine;
using View;
using View.Game.Characters;
using View.UI;

namespace Controller.Characters
{
    public class WorkersController
    {
        private WorkersModel _model;
        private WorkersView _view;
        private CharacterFactory _characterFactory;
        private List<NPCConroller> _characters = new List<NPCConroller>();

        public WorkersController() { }

        public void InitializeNew()
        {
            _model = new WorkersModel();
            SetLoaded(_model);
        }

        public void SetLoaded(WorkersModel workersModel)
        {
            _characters.ForEach(character => character.Dispose());
            _characters.Clear();
            _model = workersModel;
            _model.Workers.ForEach(worker => LoadWorker(worker));
            _model.OnWorkerAdded += OnWorkerAddedHeandler;
        }

        public WorkersModel GetWorkersModel()
        {
            _characters.ForEach(_character => _character.UpdateModel());
            return _model;
        }

        public WorkersController SetView(WorkersView view)
        {
            _view = view;
            _view.OnOpened += () => _view.UpdateView(_model.Workers, GameContext.DIContainer.Resolve<WorkshopController>().GetMaxWorkersCount());
            _view.OnWorkerUpdated += UpdateWorker;
            return this;
        }

        public WorkersController SetFactory(CharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
            return this;
        }

        public void AddWorker()
        {
            if(_model.Workers.Count < GameContext.DIContainer.Resolve<WorkshopController>().GetMaxWorkersCount())
            {
                _model.AddWorker(new WorkerModel(0, $"worker{_model.Workers.Count}"));
            }
        }

        public void AddExpForWorker(int exp, string workerId)
        {
            _model.Workers.Find(w => w.WorkerId.Equals(workerId)).AddExp(exp);
        }

        public WorkerModel GetWorker(int index)
        {
            return _model.Workers[index];
        }

        public List<string> GetWorkersData()
        {
            var result = new List<string>();
            _model.Workers.ForEach(worker => result.Add($"{worker.WorkerId} - {worker.Level}"));
            return result;
        }

        private void UpdateWorker(int index)
        {
            _model.Workers[index].UpdateLevel();//TODO сделать нормально
            _model.Workers[index].AddExp(-_model.Workers[index].Exp);
        }

        private void OnWorkerAddedHeandler(WorkerModel workerModel)
        {
            var characterModel = new CharacterModel(workerModel.WorkerId,
                CharacterType.worker,
                new Model.Items.ItemCollectionModel(workerModel.WorkerId,20));

            var character = CreateView(workerModel, characterModel, _characterFactory.StartPoint);
            _view.UpdateView(_model.Workers, GameContext.DIContainer.Resolve<WorkshopController>().GetMaxWorkersCount());
            character.CharacterStrategy.InitializeStartCommands();
        }

        private void LoadWorker(WorkerModel workerModel)
        {
            var character = CreateView(workerModel, workerModel.CharacterModel, workerModel.CharacterModel.Position);
            character.CharacterMover.gameObject.transform.eulerAngles = workerModel.CharacterModel.Rotation;
            character.InitializeLoaded();
        }

        private NPCConroller CreateView(WorkerModel workerModel, CharacterModel characterModel, Vector3 psoition)
        {
            var character = _characterFactory.GetCharacter(CharacterType.worker, psoition);
            var strategy = new WorkerStrategy().SetModel(workerModel);
            var characterController = new NPCConroller(characterModel)
                .SetView(character)
                .SetStrategy(strategy);

            workerModel.SetCharacterModel(characterModel);
            strategy.SetController(characterController);
            _characters.Add(characterController);

            return characterController;
        }
    }
}