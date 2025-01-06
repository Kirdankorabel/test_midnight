using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DI;

namespace View.UI
{
    public class QuestionPanel : ConstructableMonoBehaviour
    {
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TMP_Text _text;

        public event System.Action OnAccepted;

        public override void Construct()
        {
            _acceptButton.onClick.AddListener(Accept);
            _cancelButton.onClick.AddListener(Cancel);
            GameContext.DIContainer.Resolve<UIManager>().Register(this);
        }

        public void Open(string questionText, bool enableAcceptButton)
        {
            gameObject.SetActive(true);
            _text.text = questionText;
        }

        private void Accept()
        {
            OnAccepted?.Invoke();
            gameObject.SetActive(false);
        }

        private void Cancel()
        {
            gameObject.SetActive(false);
        }
    }
}