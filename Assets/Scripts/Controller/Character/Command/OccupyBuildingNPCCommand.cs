using UnityEngine;

namespace Controller.Characters
{
    public class OccupyBuildingNPCCommand : NPCCommand
    {
        private string _routeTag;

        public OccupyBuildingNPCCommand(string routeTag) 
        {
            _routeTag = routeTag;
        }

        public override void StartAction()
        {
            var routePoint = GameContext.DIContainer.Resolve<RouteController>().GetRoute(_routeTag).GetPoint();
            if (routePoint == null)
            {
                Break();
                EndAction();
                return;
            }
            if(routePoint.IsFree)
            {
                routePoint.IsFree = false;
                if(routePoint.building != null)
                {
                    _characterController.Model.SetBuilding(routePoint.building.PlObjectId);
                }
                else
                {
                    _characterController.Model.SetBuilding(routePoint.Id);
                }
            }
            else
            {
                Break();
            }
            EndAction();
        }
    }
}