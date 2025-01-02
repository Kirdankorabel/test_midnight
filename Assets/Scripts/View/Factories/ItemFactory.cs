using DataContainer;
using Model.Items;

namespace View
{
    public class ItemFactory : Factory<DraggebleObject>
    {
        private GameLibrary _library;

        public override void Construct()
        {
            base.Construct();
            _library = GameContext.DIContainer.Resolve<GameLibrary>();
            DraggebleObject.OnRealesed += ReleaseItem;
        }

        public DraggebleObject GetItem(string name)
        {
            var result = base.GetItem();
            result.Item = new ItemModel(_library.GetItem(name));
            result.SetSprite(result.Item.Data.Sprite);
            return result;
        }

        public DraggebleObject GetItem(ItemData itemData)
        {
            var result = base.GetItem();
            result.Item = new ItemModel(itemData);
            result.SetSprite(result.Item.Data.Sprite);
            return result;
        }

        protected override void OnApplicationQuit()
        {
            DraggebleObject.OnRealesed -= ReleaseItem;
        }
    }
}