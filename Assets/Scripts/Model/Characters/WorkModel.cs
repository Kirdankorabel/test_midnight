using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class WorkModel
    {
        [SerializeField] private List<GameTaskModel> tasks = new List<GameTaskModel>();
        [SerializeField] private int counter = 0;

        public event System.Action OnUpdated;
        public event System.Action<GameTaskModel> OnTaskAdded;

        public List<GameTaskModel> Tasks => tasks;
        public int Counter => counter;

        public void AddTask(GameTaskModel task)
        {
            counter++;
            tasks.Add(task); 
            OnTaskAdded?.Invoke(task);
        }
    }
}