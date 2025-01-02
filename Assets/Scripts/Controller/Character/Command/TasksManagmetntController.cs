using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Characters
{
    public class TasksManagmetntController
    {
        private TaskManagerModel _model;

        public event System.Action<GameTaskModel> OnTaskAdded;

        public List<GameTaskModel> Tasks => _model.Tasks;

        public void AddTask(GameTaskModel task)
        {
            _model.AddTask(task);
            OnTaskAdded?.Invoke(task);
        }

        public GameTaskModel GetTask()
        {
            var result = _model.GetTask();
            if (result == null)
            {
                
            }
            return result;
        }
    }
}