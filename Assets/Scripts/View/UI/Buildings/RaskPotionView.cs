using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.UI
{
    public class RaskPotionView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _forwardImage;
        [SerializeField] private Image _backgroundImage;

        private int _position;

        public event System.Action<int> OnPressLeft;
        public event System.Action<int> OnPressRight;

        public void SetPosition(int position)
        {
            _position = position;
        }

        public void SetForward(Sprite sprite)
        {
            _forwardImage.sprite = sprite;
        }

        public void SetBackImage(Sprite sprite)
        {
            _backgroundImage.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                OnPressLeft?.Invoke(_position);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnPressRight?.Invoke(_position);
            }
        }
    }
}