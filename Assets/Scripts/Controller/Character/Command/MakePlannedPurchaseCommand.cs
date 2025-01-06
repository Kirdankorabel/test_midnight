using System.Collections.Generic;
using UnityEngine;

namespace Controller.Characters
{
    public class MakePlannedPurchaseCommand : NPCCommand
    {
        private List<string> _targetItemIds;

        public MakePlannedPurchaseCommand(string routePoint, List<string> targetItemIds)
        {
            _targetItemIds = targetItemIds;
        }

        public override void StartAction()
        {
            var inventory = GameContext.DIContainer.Resolve<InventoryController>();
            var removedItems = new List<string>();
            for(var i = 0; i < _targetItemIds.Count; i++)
            {
                var targetItem = inventory.GetItem(_targetItemIds[i]);
                if(!targetItem.IsNullItem || !targetItem.IsFreeItem)
                {
                    removedItems.Add(_targetItemIds[i]); 
                }
                else
                {
                    inventory.ReleseItem(targetItem);
                    _characterController.ItemCollectionController.ItemCollectionModel.Items.Add(targetItem);
                }
            }
            removedItems.ForEach(item => _targetItemIds.Remove(item));
            var time = _characterController.AnimationController.PlayLookindAnimation();
            _characterController.CharacterMover.WaitTime(time, EndAction);
        }
    }
}