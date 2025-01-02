using Controller.Building;
using Model;
using System.Collections.Generic;
using UnityEngine;
using View;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

namespace Controller.Characters
{
    public class VisitController
    {
        private CharacterFactory _characterFactory;
        private TimeManager _timeManager;
        private WorkshopController _workshopController;
        private List<CharacterModel> _visitorModels;
        private List<NPCConroller> _visitors = new List<NPCConroller>();

        public VisitController()
        {
            _visitorModels = new List<CharacterModel>();
            _timeManager = GameContext.DIContainer.Resolve<TimeManager>();
            _workshopController = GameContext.DIContainer.Resolve<WorkshopController>();
        }

        public void SetFactory(CharacterFactory characterFactory, Vector3 startPosition)
        {
            _characterFactory = characterFactory;
            _timeManager.OnHourChanded += OnHourChandedHeandler;
        }

        public List<CharacterModel> GetVisitors()
        {
            _visitors.ForEach(v => v.UpdateModel());
            return _visitorModels;
        }

        public void SetLoaded(List<CharacterModel> characterModels)
        {
            _visitors.ForEach(_v => _v.Dispose());
            _visitors.Clear();
            _visitorModels.Clear();
            characterModels.ForEach(character => CreateVisitor(character));
        }

        private void OnHourChandedHeandler(int hour)
        {
            if(!_workshopController.WorkshopIsOpen || _timeManager.Hour % 2 != 0 || _timeManager.Hour < 8 
               || _timeManager.Hour > 20 || _visitorModels.Count >= _workshopController.GetMaxVisitorsCount())
            {
                return;
            }
            CreateVisitor();
        }

        private void CreateVisitor()
        {
            var character = _characterFactory.GetCharacter(CharacterType.visitor);
            var strategy = new VisitorStrategy();
            var characterModel = new CharacterModel("visitor", CharacterType.visitor, new Model.Items.ItemCollectionModel("visitor", 20));
            var characterController = new NPCConroller(characterModel).SetStrategy(strategy);
            strategy.SetController(characterController);
            character.Initialize(characterController, _characterFactory.StartPoint, "111");
            _visitorModels.Add(characterModel);
            characterModel.OnDestroyed += CharacterDestoryedHeandler;
            _visitors.Add(characterController);
        }

        private void CreateVisitor(CharacterModel characterModel)
        {
            var character = _characterFactory.GetCharacter(CharacterType.visitor);
            var strategy = new VisitorStrategy(); 
            var characterController = new NPCConroller(characterModel).SetStrategy(strategy);
            strategy.SetController(characterController);
            character.Initialize(characterController, _characterFactory.StartPoint, "111");
            character.transform.position = characterModel.Position;
            character.transform.eulerAngles = characterModel.Rotation;
            _visitorModels.Add(characterModel);
            characterModel.OnDestroyed += CharacterDestoryedHeandler;
            _visitors.Add(characterController);
            characterController.InitializeLoaded();
        }

        private void CharacterDestoryedHeandler(CharacterModel characterModel)
        {
            _visitorModels.Remove(characterModel);
            _visitors.RemoveAll(v => v.Model == characterModel);
            characterModel.OnDestroyed -= CharacterDestoryedHeandler;
        }
    }
}