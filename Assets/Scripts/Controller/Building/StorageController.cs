using Controller.Characters;
using UnityEngine;

namespace Controller.Building
{
    public class StorageController : UseablePlaceableObjectController
    {
        public StorageController(int componentCellCount)
        {
            _inventoryController = GameContext.DIContainer.Resolve<InventoryController>();
        }

        public override void SaveData()
        {
        }

        public override void Use(NPCConroller commandConroller)
        {
            Debug.LogError("UseStorage");
        }
    }
}