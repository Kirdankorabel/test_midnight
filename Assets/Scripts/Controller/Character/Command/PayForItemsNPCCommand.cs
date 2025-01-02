namespace Controller.Characters
{
    public class PayForItemsNPCCommand : NPCCommand
    {
        public override void StartAction()
        {
            GameContext.DIContainer.Resolve<AccountController>().AddMoneyFoItems(_characterController.ItemCollectionController.ItemCollectionModel.Items);
            EndAction();
        }
    }
}