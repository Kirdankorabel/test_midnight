using System.Collections.Generic;
using UnityEngine;
using View.Game.Characters;

namespace View
{
    public class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private List<Material> _materials;

        public Vector3 StartPoint => _startPoint.position;

        public Character GetCharacter(CharacterType characterType)
        {
            var result = Instantiate(_characterPrefab);
            result.GetComponent<MeshRenderer>().material = _materials[(int)characterType];
            return result;
        }

    }
}