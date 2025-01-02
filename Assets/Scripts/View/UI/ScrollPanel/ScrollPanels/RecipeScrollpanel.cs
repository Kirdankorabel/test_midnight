using DataContainer;

namespace View.UI
{
    public class RecipeScrollpanel : ScrollPanel<RecipeData>
    {
        public override void Construct()
        {
            base.Construct();
        }

        protected override void AddPanel(int index)
        {
            var newPanel = Instantiate(_horizontalPanelPrefab, _rootTransform);
            _panels.Add(newPanel);
            newPanel.OnSizeCheanged += UpdateSize;
        }
    }
}