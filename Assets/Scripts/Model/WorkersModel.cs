using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class WorkersModel
    {
        [SerializeField] List<WorkerModel> workers = new List<WorkerModel>();

        public event System.Action<WorkerModel> OnWorkerAdded;

        public List<WorkerModel> Workers => workers;

        public WorkersModel() { }

        public void AddWorker(WorkerModel worker)
        {
            workers.Add(worker);
            OnWorkerAdded(worker);
        }
    }

}