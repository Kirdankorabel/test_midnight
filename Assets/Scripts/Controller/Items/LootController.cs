using DataContainer;
using Model;
using System.Collections.Generic;
using View.UI;

namespace Controller.Building
{
    public class LootController
    {
        private LootPanel _lootPanel;
        private LootDataContainer _lootData;
        private WorkshopController _workshopController;

        public event System.Action<GameTaskModel> OnTaskCreated;

        public LootController(LootPanel lootPanel, LootDataContainer lootData)
        {
            _lootData = lootData;
            _lootPanel = lootPanel;
            _lootPanel.OnOpened += UpdateLootPanel;
            _lootPanel.OnLootCommandCreated += CreateLootTask;
            _workshopController = GameContext.DIContainer.Resolve<WorkshopController>();
        }

        public List<string> GetLoot(int count)
        {
            var result = new List<string>();
            for(var i = 0; i < count; i++)
            {
                result.Add(_lootData.GetItem(UnityEngine.Random.Range(0, _lootData.ItemCount)).ItemId);
            }
            return result;
        }

        private void CreateLootTask(int workerNumber)
        {
            if(workerNumber < 0)
            {
                return;
            }
            var task = new GameTaskModel("loot", TaskType.loot, 2).SetWorker(_workshopController.WorkersController.GetWorker(workerNumber).WorkerId);
            OnTaskCreated(task);
        }

        private void UpdateLootPanel()
        {
            _lootPanel.UpdateView(_workshopController.WorkersController.GetWorkersData());
        }
    }
}