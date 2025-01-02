using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class TaskHorizontalPanel : HorizontalPanel<GameTaskModel>
    {
        [SerializeField] private Image _image;

        public override void SetData(GameTaskModel data)
        {

        }

        public void DestroyView()
        {
            Destroy(this.gameObject);
        }
    }
}