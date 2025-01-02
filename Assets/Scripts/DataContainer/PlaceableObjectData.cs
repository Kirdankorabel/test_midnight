using UnityEngine;
using View.Game;

namespace DataContainer
{
    [System.Serializable]
    public class PlaceableObjectData : DataItem
    {
        [SerializeField] private PlaceableObject _prefab;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private bool _open = true;
        [SerializeField] private PlaceableObjectType _type;
        [SerializeField] private int _collectionSize = 5;
        [SerializeField] private int _price;

        public PlaceableObject Prefab => _prefab;
        public Sprite Sprite => _sprite;
        public bool Open => _open;
        public PlaceableObjectType PlaceableObjectType => _type;
        public int CollectionSize => _collectionSize;
        public int Price => _price;
    }
}