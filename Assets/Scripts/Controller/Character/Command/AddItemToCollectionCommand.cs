using Controller.Building;
using Model.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Characters
{
    public class AddItemToCollectionCommand : NPCCommand
    {
        [SerializeField] private string pointTag;
        [SerializeField] private string buildingId;
        [SerializeField] private List<string> itemIds;

        public AddItemToCollectionCommand(string pointTag, string buildingId, List<string> itemIds)
        {
            this.pointTag = pointTag;
            this.buildingId = buildingId;
            this.itemIds = itemIds;
        }

        public override void StartAction()
        {
            if (buildingId == null)
            {
                var point = GameContext.DIContainer.Resolve<RouteController>().GetRoute(pointTag).GetPoint();
                var items = _characterController.ItemCollectionController.GetItems(itemIds);
                var targetItems = itemIds.FindAll(i => !string.IsNullOrEmpty(i));

                if (point == null || point.PlaceableObjectController == null || items.Count < targetItems.Count)
                {
                    Break();
                    return;
                }
                items.ForEach(item => AddItemToCollection(item, point.PlaceableObjectController));
            }
            else
            {
                var point = GameContext.DIContainer.Resolve<RouteController>().GetRoute(buildingId, pointTag).GetPoint();
                var items = _characterController.ItemCollectionController.GetItems(itemIds);
                var targetItems = itemIds.FindAll(i => !string.IsNullOrEmpty(i));

                if (point == null || point.PlaceableObjectController == null || items.Count < targetItems.Count)
                {
                    Break();
                    return;
                }
                items.ForEach(item => AddItemToCollection(item, point.PlaceableObjectController));
            }

            EndAction();
        }

        private void AddItemToCollection(ItemModel item, UseablePlaceableObjectController controller)
        {
            controller.ItemCollectionController.AddItem(item);
            _characterController.ItemCollectionController.ReleseItem(item);
            item.IsFreeItem = true;
        }
    }
}