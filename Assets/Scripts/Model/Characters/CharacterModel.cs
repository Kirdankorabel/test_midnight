using Model.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class CharacterModel
    {
        [SerializeField] private string id;
        [SerializeField] private string buildingId;
        [SerializeField] private CharacterType characterType;
        [SerializeField] private GameTaskModel taskModel;
        [SerializeField] private ItemCollectionModel itemCollectionModel;
        [SerializeField] private CommandModel lastCommand;
        [SerializeField] private List<CommandModel> commandModels;
        [SerializeField] private Vector3 position;
        [SerializeField] private Vector3 rotation;

        public event System.Action<CharacterModel> OnDestroyed;

        public string Id => id;
        public GameTaskModel GameTaskModel => taskModel;
        public int CommandCount => commandModels.Count;
        public CommandModel LastCommand => lastCommand;
        public string BuildingId => buildingId;
        public ItemCollectionModel ItemCollectionModel => itemCollectionModel;

        public Vector3 Position
        {
            get => position;
            set => position = value;
        }
        public Vector3 Rotation
        {
            get => rotation;
            set => rotation = value;
        }

        public CharacterModel(string id, CharacterType characterType, ItemCollectionModel itemCollectionModel)
        {
            this.id = id;
            this.characterType = characterType;
            this.itemCollectionModel = itemCollectionModel;
            commandModels = new List<CommandModel>();
        }

        public void SetTaskModel(GameTaskModel taskModel)
        {
            this.taskModel = taskModel;
        }

        public void SetBuilding(string buildingId)
        {
            this.buildingId = buildingId;
        }

        public void AddCommand(CommandModel commandModel)
        {
            commandModels.Add(commandModel);
        }

        public CommandModel GetCommand()
        {
            var result = commandModels[0];
            lastCommand = result;
            commandModels.Remove(lastCommand);
            return result;
        }

        public void ClearCommands()
        {
            commandModels.Clear();
        }

        public void Destroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}