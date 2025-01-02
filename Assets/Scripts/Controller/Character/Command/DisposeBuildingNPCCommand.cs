namespace Controller.Characters
{
    public class DisposeBuildingNPCCommand : NPCCommand
    {
        private string _routeTag;

        public DisposeBuildingNPCCommand(string routeTag)
        {
            _routeTag = routeTag;
        }

        public override void StartAction()
        {
            var routePoint = GameContext.DIContainer.Resolve<RouteController>().
                GetRoute(_characterController.Model.BuildingId, _routeTag).GetPoint();
            routePoint.IsFree = true;
            _characterController.Model.SetBuilding(string.Empty);
            EndAction();
        }
    }
}