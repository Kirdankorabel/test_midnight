using System.Collections.Generic;
using UnityEngine;

namespace View.UI
{
    public class Closer : MonoBehaviour
    {
        private static Queue<ICloseable> _closeableObjects = new Queue<ICloseable>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _closeableObjects.Count > 0)
            {
                ICloseable closeable = _closeableObjects.Dequeue();
                while (!closeable.IsOpen && _closeableObjects.Count > 0)
                {
                    closeable.Close();
                    closeable = _closeableObjects.Dequeue();
                }
                closeable.Close();
            }
        }

        public static void AddCloseablle(ICloseable closeable)
        {
            _closeableObjects.Enqueue(closeable);
        }

        private void OnEnable()
        {
            _closeableObjects.Clear();
        }
    }
}