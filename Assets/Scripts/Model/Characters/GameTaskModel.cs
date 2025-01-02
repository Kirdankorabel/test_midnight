using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class GameTaskModel
    {
        [SerializeField] private string taskId;
        [SerializeField] private TaskType type;
        [SerializeField] private TaskStatus taskStatus;
        [SerializeField] private string recipeId;
        [SerializeField] private string buildingId;
        [SerializeField] private int exp;
        [SerializeField] private string workerId;

        public event System.Action OnStatusUpdated;

        public string TaskId => taskId;
        public string WorkerId => workerId;
        public TaskType Type => type;
        public TaskStatus TaskStatus => taskStatus;
        public string RecipeId => recipeId;
        public string BuildingId => buildingId;
        public int Exp => exp;

        public GameTaskModel(string itemId, TaskType type, int exp)
        {
            this.exp = exp;
            this.taskId = itemId;
            this.type = type;
            taskStatus = TaskStatus.Waite;
            workerId = string.Empty;
        }

        public GameTaskModel SetRecipe(string recipeId)
        {
            this.recipeId = recipeId;
            return this;
        }

        public GameTaskModel SetStatus(TaskStatus taskStatus)
        {
            this.taskStatus = taskStatus;
            OnStatusUpdated?.Invoke();
            return this;
        }

        public GameTaskModel SetBuilding(string buildingId)
        {
            this.buildingId = buildingId;
            return this;
        }

        public GameTaskModel SetWorker(string workerId)
        {
            this.workerId = workerId;
            return this;
        }
    }
}