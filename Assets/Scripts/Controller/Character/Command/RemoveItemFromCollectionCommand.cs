using System.Collections.Generic;
using UnityEngine;

namespace Controller.Characters
{
    public class RemoveItemFromCollectionCommand : NPCCommand
    {
        private string _pointTag;
        private string _buildingId;
        private List<string> _itemIds;

        public RemoveItemFromCollectionCommand(string pointTag, string buildingId, List<string> itemIds)
        {
            this._pointTag = pointTag;
            this._buildingId = buildingId;
            this._itemIds = itemIds;
        }

        public override void StartAction()
        {
            if(!string.IsNullOrEmpty(_characterController.Model.BuildingId))
            {
                var point = GameContext.DIContainer.Resolve<RouteController>().GetRoute(_characterController.Model.BuildingId, _pointTag).GetPoint();
                if(point == null)
                {
                    Debug.LogError($"{_characterController.Model.BuildingId} {_pointTag}");
                    _characterController.Model.SetBuilding(string.Empty);
                    Break();
                    return;
                }
                var targetItems = _itemIds.FindAll(i => !string.IsNullOrEmpty(i));
                var items = point.PlaceableObjectController.ItemCollectionController.GetItems(_itemIds);
                if (items.Count < targetItems.Count)
                {
                    Break();
                    return;
                }
                items.ForEach(item => point.PlaceableObjectController.ItemCollectionController.ReleseItem(item));
                items.ForEach(item => _characterController.ItemCollectionController.AddItem(item));
            }
            else
            {
                var point = GameContext.DIContainer.Resolve<RouteController>().GetRoute(_pointTag).GetPoint();
                var items = point.PlaceableObjectController.ItemCollectionController.GetItems(_itemIds);
                var targetItems = _itemIds.FindAll(i => !string.IsNullOrEmpty(i));
                if (point == null || items.Count < targetItems.Count)
                {
                    Break();
                    return;
                }
                items.ForEach(item => point.PlaceableObjectController.ItemCollectionController.ReleseItem(item));
                items.ForEach(item => _characterController.ItemCollectionController.AddItem(item));
            }
            EndAction();
        }
    }
}