using Model;
using System.Collections.Generic;
using UnityEngine;

namespace DataContainer
{
    [CreateAssetMenu(fileName = "AlcemicalPlantDataContainer", menuName = "ScriptableObjects/DataContainer/AlcemicalPlantDataContainer", order = 1)]
    public class AlcemicalPlantDataContainer : ScriptableObject
    {
        [SerializeField] private List<AlcemicalPlantData> alcemicalPlantDatas;

        public List<AlcemicalPlantData> Data => alcemicalPlantDatas;

        public AlcemicalPlantData GetAlcemicalPlantData(CraftType type)
        {
            return alcemicalPlantDatas.Find(d => d.CraftType == type);
        }

        public AlcemicalPlantData GetAlcemicalPlantData(string name)
        {
            return alcemicalPlantDatas.Find(d => d.Name.Equals(name));
        }
    }

    [System.Serializable]
    public class AlcemicalPlantData
    {
        [SerializeField] private List<CellData> plantCellDatas;
        [SerializeField] private string name;
        [SerializeField] private CraftType craftType;
        [SerializeField] private Sprite sprite;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector3 offset;
        [SerializeField] private int componentCount;

        public int CellCount => plantCellDatas.Count;
        public List<CellData> PlantCellDatas => plantCellDatas;
        public string Name => name;
        public CraftType CraftType => craftType;
        public Sprite Sprite => sprite;
        public GameObject Prefab => prefab;
        public Vector3 Offset => offset;
        public int ComponentCount => componentCount;
    }
}