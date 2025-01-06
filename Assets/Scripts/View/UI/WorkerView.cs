using Controller;
using DataContainer;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class WorkerView : MonoBehaviour
    {
        [SerializeField] private WorkerDataContainer _workerDataContainer;
        [SerializeField] private Button _updateWorkerButton;
        [SerializeField] private Image _image;
        [SerializeField] private Image _fillImage;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _expText;
        [SerializeField] private TMP_Text _nameText;

        private AccountController _accountController;

        public event System.Action OnWorkerUpdated;

        private void Start()
        {
            _updateWorkerButton.onClick.AddListener(() => OnWorkerUpdated?.Invoke());
        }

        public void UpdateView(WorkerModel workerModel)
        {
            _accountController = _accountController == null ? GameContext.DIContainer.Resolve<AccountController>() : _accountController;
            _levelText.text = workerModel.Level.ToString();
            _nameText.text = workerModel.WorkerId;
            if (workerModel.Level < _workerDataContainer.MaxLevel)
            {
                _fillImage.fillAmount = (float)(workerModel.Exp / _workerDataContainer.ExpForLevel[workerModel.Level]);
                _updateWorkerButton.interactable = 
                    _accountController.Balance > _workerDataContainer.PriceFoLevel[workerModel.Level]
                    && workerModel.Exp > _workerDataContainer.ExpForLevel[workerModel.Level];
                _expText.text = $"{workerModel.Exp} / {_workerDataContainer.ExpForLevel[workerModel.Level]}";
            }
            else
            {
                _fillImage.fillAmount = 1f;
                _updateWorkerButton.interactable = false;
            }
        }
    }
}