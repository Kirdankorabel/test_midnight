using System.Collections.Generic;

namespace Controller.Characters
{
    public class MakePlannedPurchaseCommand : NPCCommand
    {
        private List<string> _targetItemIds;
        private string _routePoint;

        public MakePlannedPurchaseCommand(string routePoint, List<string> targetItemIds)
        {
            _routePoint = routePoint;
            _targetItemIds = targetItemIds;
        }

        public override void StartAction()
        {
            var i = 0;
            var inventory = GameContext.DIContainer.Resolve<InventoryController>();
            _targetItemIds.ForEach(item =>
            {
                var targetItem = inventory.GetItem(item);
                if(!targetItem.IsNullItem || !targetItem.IsFreeItem)
                {
                    _targetItemIds.Remove(item); 
                }
                else
                {
                    inventory.ReleseItem(targetItem);
                    _characterController.ItemCollectionController.ItemCollectionModel.Items.Add(targetItem);
                }
            });
            _characterController.CharacterMover.WaitToTime(0.1f, EndAction);
            EndAction();
        }
    }
}