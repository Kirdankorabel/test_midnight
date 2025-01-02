using Controller.Characters;
using Model;

namespace View.UI
{
    public class WorkView : ScrollPanel<GameTaskModel>
    {
        public event System.Action<GameTaskModel> OnTaskRemoved;

        public override void Construct()
        {
            base.Construct();
        }

        public TaskView CreatePanelForTask(GameTaskModel model)
        {
            var newPanel = Instantiate(_horizontalPanelPrefab, _rootTransform) as TaskView;
            _panels.Add(newPanel);
            newPanel.OnSizeCheanged += UpdateSize;
            newPanel.OnRightClick += () => OnTaskRemoved?.Invoke(model);
            newPanel.SetData(model);
            return (TaskView)newPanel;
        }
    }
}