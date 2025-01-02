using UnityEngine;

namespace View.UI
{
    public abstract class ClickableObject : MonoBehaviour, IClickable
    {
        public abstract void Click();
    }
}