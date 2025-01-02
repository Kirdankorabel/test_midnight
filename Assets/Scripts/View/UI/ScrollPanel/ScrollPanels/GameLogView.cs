namespace View.UI
{

    public class GameLogView : ScrollPanel<string>
    {
        public void PrintMassage(string text)
        {
            var newPanel = Instantiate(_horizontalPanelPrefab, _rootTransform);
            newPanel.SetData(text);
            _panels.Add(newPanel);
            UpdateSize();
        }
    }
}