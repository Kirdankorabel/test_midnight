using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class RightPanel : MonoBehaviour
    {
        [SerializeField] private Button _openInventoryButton;
        [SerializeField] private Button _openWorkersViewButton;
        [SerializeField] private Button _openReseacth;
        [SerializeField] private PlayerInventoryView _playerInventoryView;
        [SerializeField] private WorkersView _workersView;
        [SerializeField] private ReseacrhItemCollectionView _reseacrhItemCollectionView;

        private void Start()
        {
            _openInventoryButton.onClick.AddListener(OpenInventory);
            _openWorkersViewButton.onClick.AddListener(OpenWorkersPanel);
            _openReseacth.onClick.AddListener(OpenReseacth);
        }

        private void OpenInventory()
        {
            _playerInventoryView.Open();
        }

        private void OpenWorkersPanel()
        {
            _workersView.Open();
        }

        private void OpenReseacth()
        {
            _reseacrhItemCollectionView.Open();
        }
    }
}