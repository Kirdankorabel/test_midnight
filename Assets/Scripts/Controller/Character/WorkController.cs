using Controller.Building;
using DataContainer;
using Model;
using View.UI;

namespace Controller.Characters
{
    public class WorkController
    {
        private WorkModel _model;
        private WorkView _view;
        private LootController _lootController;

        public event System.Action OnUpdated;
        public event System.Action OnTaskAdded;

        public WorkModel Model => _model;
        public LootController LootController => _lootController;

        public void InitializeNew()
        {
            _model = new WorkModel();
            _model.OnTaskAdded += OnTaskAddedHeandler;
        }

        public void SetLoaded(WorkModel model)
        {
            if(_model != null)
            {
                _model.OnTaskAdded -= OnTaskAddedHeandler;
            }
            _model = model;
            _model.OnTaskAdded += OnTaskAddedHeandler;
            _model.Tasks.ForEach(task => _view.CreatePanelForTask(task));
        }

        public WorkController SetLootController(LootController lootController)
        {
            _lootController = lootController;
            _lootController.OnTaskCreated += AddTask;
            return this;
        }

        public WorkController SetView(WorkView view)
        {
            _view = view;
            _view.OnTaskRemoved += RemoveTask;
            return this;
        }

        public WorkController SetLootContoller(LootController lootController)
        {
            _lootController = lootController;
            return this;
        }

        public void AddTask(RecipeData recipe)
        {
            var taskModel = new GameTaskModel("task" + _model.Counter, TaskType.craft, 2).SetRecipe(recipe.ItemId);
            _model.AddTask(taskModel);
        }

        public void AddTask(string buildingId)
        {
            var taskModel = new GameTaskModel("task" + _model.Counter, TaskType.delivery, 1).SetBuilding(buildingId);
            _model.AddTask(taskModel);
        }

        public void AddTask(GameTaskModel taskModel)
        {
            _model.AddTask(taskModel);
        }

        public GameTaskModel GetFreeTask(string workerId)
        {
            GameTaskModel result;
            if (!string.IsNullOrEmpty(workerId))
            {
                result = _model.Tasks.Find(t => t.TaskStatus == TaskStatus.Waite && t.WorkerId.Equals(workerId));
                if(result != null)
                {
                    return result;
                }
            }
            result = _model.Tasks.Find(t => t.TaskStatus == TaskStatus.Waite);
            return result;
        }

        private void OnTaskAddedHeandler(GameTaskModel model)
        {
            var panel = _view.CreatePanelForTask(model);
            model.OnStatusUpdated += () => UpdateTaskStatus(model, panel);
            OnTaskAdded?.Invoke();
        }

        private void RemoveTask(GameTaskModel model)
        {
            model.SetStatus(TaskStatus.Failed);
        }

        private void UpdateTaskStatus(GameTaskModel model, TaskView taskView)
        {
            taskView.SetData(model);
        }
    }
}