using Controller.Building;
using UnityEngine;
using View;

namespace Controller.Characters
{
    public class CreateLootNPCCommand : NPCCommand
    {
        private WorkController _workController;
        private InventoryController _inventoryController;
        private GameLibrary _gameLibrary;

        public CreateLootNPCCommand(float time)
        {
            _inventoryController = GameContext.DIContainer.Resolve<InventoryController>();
            _workController = GameContext.DIContainer.Resolve<WorkController>();
            _gameLibrary = GameContext.DIContainer.Resolve<GameLibrary>();
        }

        public override void StartAction()
        {
            EndAction();
        }

        public override void EndAction()
        {
            var loot = _workController.LootController.GetLoot(10);
            loot.ForEach(item => _inventoryController.AddItem(_gameLibrary.GetItem(item)));           
            base.EndAction();
        }
    }
}