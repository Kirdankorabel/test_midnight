using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class TaskManagerModel
    {
        [SerializeField] private List<GameTaskModel> tasks = new List<GameTaskModel>();

        public List<GameTaskModel> Tasks => tasks;

        public TaskManagerModel() { }

        public void AddTask(GameTaskModel task)
        {
            tasks.Add(task);
        }

        public GameTaskModel GetTask() 
        {
            return tasks.Find(t => t.TaskStatus == TaskStatus.Waite);
        }
    }
}