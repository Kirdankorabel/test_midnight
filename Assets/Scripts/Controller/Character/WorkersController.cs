using Controller.Building;
using Model;
using System.Collections.Generic;
using View;
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

        private void OnWorkerAddedHeandler(WorkerModel workerModel)
        {
            var character = _characterFactory.GetCharacter(CharacterType.worker);
            var strategy = new WorkerStrategy().SetModel(workerModel);
            var characterModel = new CharacterModel(workerModel.WorkerId, CharacterType.worker, new Model.Items.ItemCollectionModel(workerModel.WorkerId,20));
            var characterController = new NPCConroller(characterModel).SetStrategy(strategy);
            workerModel.SetCharacterModel(characterModel);
            strategy.SetController(characterController);
            character.Initialize(characterController, _characterFactory.StartPoint, "111"); 
            _view.UpdateView(_model.Workers, GameContext.DIContainer.Resolve<WorkshopController>().GetMaxWorkersCount());
            _characters.Add(characterController);
        }

        private void LoadWorker(WorkerModel workerModel)
        {
            var character = _characterFactory.GetCharacter(CharacterType.worker);
            var strategy = new WorkerStrategy().SetModel(workerModel);
            var characterController = new NPCConroller(workerModel.CharacterModel).SetStrategy(strategy);
            strategy.SetController(characterController);
            character.Initialize(characterController, workerModel.Position, "111");
            characterController.InitializeLoaded();
            character.transform.position = workerModel.Position;
            character.transform.eulerAngles = workerModel.Rotation;
            _characters.Add(characterController);
        }
    }
}