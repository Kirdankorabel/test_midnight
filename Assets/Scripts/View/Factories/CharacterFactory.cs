using System.Collections.Generic;
using UnityEngine;
using View.Game.Characters;

namespace View
{
    public class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private List<CharacterView> _prefabs;

        public Vector3 StartPoint => _startPoint.position;

        public CharacterView GetCharacter(CharacterType characterType, Vector3 psoition)
        {
            var result = Instantiate(_prefabs[(int)characterType], psoition, Quaternion.identity, transform);
            return result;
        }

    }
}