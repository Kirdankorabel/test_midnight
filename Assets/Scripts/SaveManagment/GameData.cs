using Model;
using Model.Building;
using Model.Items;
using System;
using System.Collections.Generic;

namespace SaveManagment
{
    [System.Serializable]
    public class GameData
    {
        public float time;
        public string name;
        public string dateTime;
        public ItemCollectionModel playerInventory;
        public List<RoomModel> rooms;
        public WorkersModel workers;
        public WorkModel work;
        public AccountModel account;
        public List<CharacterModel> visitors;

        public System.DateTime GetDateTime => System.DateTime.Parse(dateTime);
    }
}