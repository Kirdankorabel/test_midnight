using Controller.Characters;

namespace Controller.Building
{
    public abstract class UseablePlaceableObjectController : PlaceableObjectController
    {
        public bool IsFree { get; private set; }
        public float Time { get; private set; }
        public abstract void Use(NPCConroller commandConroller);
    }
}