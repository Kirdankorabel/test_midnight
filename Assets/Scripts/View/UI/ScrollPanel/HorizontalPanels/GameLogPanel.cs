using TMPro;
using UnityEngine;

namespace View.UI
{
    public class GameLogPanel : HorizontalPanel<string>
    {
        [SerializeField] private TMP_Text _massageText;

        public override void SetData(string data)
        {
            _massageText.text = data;
        }
    }
}