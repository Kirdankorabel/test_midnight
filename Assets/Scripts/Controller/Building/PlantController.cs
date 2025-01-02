using Controller.Characters;
using Model.Items;
using System.Collections.Generic;
using View;

namespace Controller.Building
{
    public class PlantController : UseablePlaceableObjectController
    {
        private int _componentCellCount;
        private GameLibrary _library;

        public PlantController(int componentCellCount)
        {
            _componentCellCount = componentCellCount;
            _inventoryController = new ItemCollectionController(new ItemCollectionModel("plant", componentCellCount + 1));
            _library = GameContext.DIContainer.Resolve<GameLibrary>();
        }

        public override void SaveData()
        {
        }

        public override void Use(NPCConroller user)
        {
            var components = new List<string>();
            for (var i = 0; i < _componentCellCount; i++)
            {
                if(!_inventoryController.GetItem(i).IsNullItem)
                {
                    components.Add(_inventoryController.GetItem(i).ItemId);
                    _inventoryController.ReleseItem(i);
                }
            }
            var product = _library.GetProduct(components);
            _inventoryController.TryToSetItem(new ItemModel(product), _componentCellCount);
        }
    }
}