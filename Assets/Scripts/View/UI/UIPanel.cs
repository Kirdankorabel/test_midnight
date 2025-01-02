using System;
using UnityEngine;

namespace View.UI
{
    public abstract class UIPanel : ConstructableMonoBehaviour
    {
        [SerializeField] private string _panelName;

        public string PanelName => _panelName;
    }
}