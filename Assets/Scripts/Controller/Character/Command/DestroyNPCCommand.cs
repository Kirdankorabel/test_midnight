namespace Controller.Characters
{
    public class DestroyNPCCommand : NPCCommand
    {
        public DestroyNPCCommand() { }

        public override void StartAction()
        {
            _characterController.Dispose();
        }
    }
}