using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class AccountModel
    {
        [SerializeField] private int balance;
        [SerializeField] private List<string> openedRecipes;
        [SerializeField] private List<WorkerModel> workerModels;

        public int Balance => balance;
        public List<string> OpenedRecipes => openedRecipes;

        public event System.Action<int> OnBalanceUpdated;

        public AccountModel() { }

        public void UpdateBalance(int value)
        {
            this.balance += value;
            OnBalanceUpdated?.Invoke(this.balance);
        }

        public void OpenRecipe(string recipe)
        {
            openedRecipes.Add(recipe);
        }
    }
}