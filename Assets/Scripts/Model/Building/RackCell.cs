using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class RackCell
    {
        [SerializeField] private bool isStatic;
        [SerializeField] private string potionId = string.Empty;

        public bool IsStatic => isStatic;
        public string ItemInCell => potionId;

        public RackCell() { }

        public void SetStatic(bool isStatic)
        {
            this.isStatic = isStatic;
        }

        public void SetPotion(string potionId)
        {
            this.potionId = potionId;
        }
    }
}