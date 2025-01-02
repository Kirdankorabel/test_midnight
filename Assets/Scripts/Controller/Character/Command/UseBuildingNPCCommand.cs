using Controller.Building;
using System;

namespace Controller.Characters
{
    public class UseBuildingNPCCommand : NPCCommand
    {
        private Action _action;

        public UseBuildingNPCCommand() { }

        public override void StartAction()
        {
            _characterController.CharacterMover.WaitToTime(0.01f, EndAction);
        }

        public override void EndAction()
        {
            _action?.Invoke();
            var building = GameContext.DIContainer.Resolve<WorkshopController>()
                .GetBuilding(_characterController.Model.BuildingId) 
                as UseablePlaceableObjectController;
            building.Use(_characterController);
            base.EndAction();
        }

        public UseBuildingNPCCommand AddAction(Action action)
        {
            _action = action;
            return this;    
        }
    }
}