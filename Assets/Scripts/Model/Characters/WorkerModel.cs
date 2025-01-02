using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class WorkerModel
    {
        [SerializeField] private CharacterModel characterModel;
        [SerializeField] private int level;
        [SerializeField] private string workerId;
        [SerializeField] private int exp;
        [SerializeField] private Vector3 position;
        [SerializeField] private Vector3 rotation;

        public event System.Action OnExpAdded;
        public event System.Action OnLevelUpdated;

        public string WorkerId => workerId;
        public int Level => level;
        public int Exp => exp;
        public Vector3 Position => position;
        public Vector3 Rotation => rotation;
        public CharacterModel CharacterModel => characterModel;

        public WorkerModel(int level, string name)
        {
            this.level = level;
            this.workerId = name;
        }

        public void AddExp(int exp)
        {
            this.exp += exp;
            OnExpAdded?.Invoke();
        }

        public WorkerModel SetCharacterModel(CharacterModel characterModel)
        {
            this.characterModel = characterModel;
            return this;
        }

        public void UpdateLevel()
        {
            level++;
        }

        public void UpdatePosition(Vector3 pos)
        {
            position = pos;
        }

        public void UpdateRotation(Vector3 rotation)
        {
            this.rotation = rotation;
        }
    }
}