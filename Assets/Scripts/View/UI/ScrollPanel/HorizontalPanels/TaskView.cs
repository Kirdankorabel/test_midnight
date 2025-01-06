using Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.UI
{
    public class TaskView : HorizontalPanel<GameTaskModel>, IPointerClickHandler
    {
        [SerializeField] private List<Color> _backgroundColors;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _lootSprite;
        [SerializeField] private Sprite _deliverySprite;

        private GameLibrary _library;

        public event System.Action OnRightClick;

        public override void SetData(GameTaskModel data)
        {
            _library = GameContext.DIContainer.Resolve<GameLibrary>();
            if(data.Type == TaskType.craft)
            {
                _image.sprite = _library.GetProduct(data.RecipeId).Sprite;
            }
            switch (data.Type)
            {
                case TaskType.craft:
                    _image.sprite = _library.GetProduct(data.RecipeId).Sprite;
                    break;
                case TaskType.delivery:
                    _image.sprite = _deliverySprite;
                    break;
                case TaskType.loot:
                    _image.sprite = _lootSprite;
                    break;


            }
            _backgroundImage.color = _backgroundColors[(int)data.TaskStatus];

            if (data.TaskStatus == TaskStatus.Success || data.TaskStatus == TaskStatus.Failed)
            {
                DestroyTaskView();
            }
        }

        public void DestroyTaskView()
        {
            OnRightClick = null;
            try
            {
                Destroy(this.gameObject);
            }
            catch 
            { 
                //TODO найти где он дестроится
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick?.Invoke();
            }
        }
    }
}