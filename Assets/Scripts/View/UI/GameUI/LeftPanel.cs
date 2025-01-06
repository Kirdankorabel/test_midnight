using DataContainer;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class LeftPanel : ConstructableMonoBehaviour
    {
        [SerializeField] private Button _openPanelButton;
        [SerializeField] private RecipeScrollpanel _recipeScrollPanel;
        [SerializeField] private RecipeDataContainer _recipeDataContainer;
        [SerializeField] private WorkView _workView;

        public override void Construct()
        {
            _openPanelButton.onClick.AddListener(Open);
            _recipeScrollPanel.Construct();
            _workView.Construct();
            _workView.Open();
        }

        private void Open()
        {
            _recipeScrollPanel.SetData(_recipeDataContainer.Data);
            _recipeScrollPanel.Open();
        }
    }
}