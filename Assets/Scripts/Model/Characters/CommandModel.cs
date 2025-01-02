using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class CommandModel
    {
        [SerializeField] private CommandType type;
        [SerializeField] private string pointTag;
        [SerializeField] private string buildingId;
        [SerializeField] private List<string> itemIds;
        [SerializeField] private int exp;
        [SerializeField] private float time;

        public string PointTag => pointTag;
        public string BuildingId => buildingId;
        public CommandType Type => type;
        public List<string> ItemIds => itemIds;
        public int Exp => exp;
        public float Time => time;

        public CommandModel() { }
        public CommandModel(CommandType type) 
        {
            this.type = type;
        }

        public CommandModel SetPointTag(string pointTag)
        {
            this.pointTag = pointTag;
            return this;
        }

        public CommandModel SetBuildingId(string buildingId)
        {
            this.buildingId = buildingId;
            return this;
        }

        public CommandModel SetItems(List<string> itemIds)
        {
            this.itemIds = itemIds;
            return this;
        }

        public CommandModel SetExp(int exp)
        {
            this.exp = exp;
            return this;
        }

        public CommandModel SetTime(float time)
        {
            this.time = time;
            return this;
        }
    }
}