using Model.Items;
using UnityEngine;

namespace Model.Building
{
    [System.Serializable]
    public class PlaceableObjectModel
    {
        [SerializeField] protected string id;
        [SerializeField] protected string placeableObjectId;
        [SerializeField] protected int position;
        [SerializeField] protected ItemCollectionModel itemCollection;
        [SerializeField] protected string data;

        public event System.Action OnItemAdded;

        public ItemCollectionModel ItemCollectionModel => itemCollection;
        public string Id => id;
        public string PlaceableObjectType => placeableObjectId;
        public int Positiion => position;
        public string Data => data;

        public PlaceableObjectModel(string id) 
        {
            this.id = id;
        }

        public PlaceableObjectModel SetItemCollection(ItemCollectionModel itemCollection)
        {
            this.itemCollection = itemCollection;
            return this;
        }

        public PlaceableObjectModel SetType(string placeableObjectId)
        {
            this.placeableObjectId = placeableObjectId;
            return this;
        }

        public PlaceableObjectModel SetPosition(int positiion)
        {
            this.position= positiion;
            return this;
        }

        public PlaceableObjectModel SetData(string data)
        {
            this.data = data;
            return this;
        }
    }
}