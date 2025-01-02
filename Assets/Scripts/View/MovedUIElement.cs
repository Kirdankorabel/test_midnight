using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class MovedUIElement : MonoBehaviour, ICloseable
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Sprite _opened;
        [SerializeField] private Sprite _closed;


        public bool InEndPosition { get; private set; }

        public bool IsOpen => InEndPosition;

        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _moveButton.onClick.AddListener(Move);
        }

        private void Move()
        {
            if(InEndPosition)
            {
                MoveToStartPosition();
            }
            else
            {
                MoveToEndPosition();
            }
        }

        public void MoveToEndPosition()
        {
            Closer.AddCloseablle(this);
            _moveButton.image.sprite = _opened;
            _rectTransform.anchoredPosition = _endPosition;
            InEndPosition = true;
        }

        public void MoveToStartPosition()
        {
            _moveButton.image.sprite = _closed;
            _rectTransform.anchoredPosition = _startPosition;
            InEndPosition = false;
        }

        public void Close()
        {
            MoveToStartPosition();
        }
    }
}