using Controller.Building;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class TopPanel : ConstructableMonoBehaviour
    {
        [SerializeField] private Button _buildingButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _openButton;

        private WorkshopController _workchopController;

        public override void Construct()
        {
            _workchopController = GameContext.DIContainer.Resolve<WorkshopController>();
            _buildingButton.onClick.AddListener(EnableBuildingMode);
            _openButton.onClick.AddListener(ChangeDoorState);
        }

        private void EnableBuildingMode()
        {
            _workchopController.ChangeBuildingMode();
        }

        private void ChangeDoorState()
        {
            _workchopController.ChangeDoorState();
        }
    }
}