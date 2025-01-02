using TMPro;
using UnityEngine;

namespace View.UI
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;

        public void UpdateView(int money)
        {
            _moneyText.text = money.ToString();
        }
    }
}