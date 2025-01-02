using System.Collections.Generic;
using UnityEngine;

namespace View.UI
{
    public class CellFactory : MonoBehaviour
    {
        [SerializeField] private Cell _cellPrefab;

        public List<Cell> SpawnCells(Transform rootTransform, int count, bool enableDrugging)
        {
            var cells = new List<Cell>();
            for (var i = 0; i < count; i++)
            {
                cells.Add(Instantiate(_cellPrefab, rootTransform));
                cells[i].EnableDragging(enableDrugging);
            }
            return cells;
        }
    }
}