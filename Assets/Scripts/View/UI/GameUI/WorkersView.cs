using Controller.Building;
using Model;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class WorkersView : ConstructableMonoBehaviour, ICloseable
    {
        [SerializeField] private WorkerView _workerViewPrefab;
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Button _addWorkerButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _maxWorkerText;

        private WorkshopController _workshopController;
        private List<WorkerView> _workerViewsList = new List<WorkerView>();

        public event System.Action<int> OnWorkerUpdated;

        public bool IsOpen => gameObject.active;

        public event System.Action OnOpened;

        public override void Construct()
        {
            _workshopController = GameContext.DIContainer.Resolve<WorkshopController>();
            _addWorkerButton.onClick.AddListener(_workshopController.WorkersController.AddWorker);
            _closeButton.onClick.AddListener(Close);
        }

        public void Open()
        {
            Closer.AddCloseablle(this);
            gameObject.SetActive(true);
            OnOpened?.Invoke();
        }

        public void UpdateView(List<WorkerModel> workerModels, int maxWorkerCount)
        {
            WorkerView workerView;
            for (int i = 0; i < workerModels.Count; i++)
            {
                workerView = i < _workerViewsList.Count ? _workerViewsList[i] : CreateNew();
                workerView.UpdateView(workerModels[i]);
                var workerIndex = i;
                workerView.OnWorkerUpdated += () => OnWorkerUpdated(workerIndex);
            }
            _maxWorkerText.text = $"{workerModels.Count} / {maxWorkerCount}";
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private WorkerView CreateNew()
        {
            var result = Instantiate(_workerViewPrefab, _rootTransform);
            _workerViewsList.Add(result);
            return result;
        }
    }
}