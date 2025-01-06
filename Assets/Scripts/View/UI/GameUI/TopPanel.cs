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
        [SerializeField] private Color _color1;
        [SerializeField] private Color _color2;

        private bool _enabled = false;
        private WorkshopController _workchopController;

        public override void Construct()
        {
            //TODO переписать нормально. ¬се в правую панель
            _workchopController = GameContext.DIContainer.Resolve<WorkshopController>();
            _buildingButton.onClick.AddListener(EnableBuildingMode);
            _openButton.onClick.AddListener(ChangeDoorState);
            _openButton.image.color = _enabled ? _color1 : _color2;
        }

        public void UpdateDoorColor()
        {
            _enabled = !_enabled;
            _openButton.image.color = _enabled ? _color1 : _color2;
        }

        private void EnableBuildingMode()
        {
            _workchopController.ChangeBuildingMode();
        }

        private void ChangeDoorState()
        {
            _workchopController.ChangeDoorState();
            UpdateDoorColor();
        }
    }
}