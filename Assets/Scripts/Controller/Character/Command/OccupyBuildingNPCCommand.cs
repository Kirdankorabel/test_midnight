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
                _characterController.Model.SetBuilding(routePoint.controller.PlObjectId);
            }
            else
            {
                UnityEngine.Debug.LogError($"point {routePoint} is occupy");
                Break();
            }
            EndAction();
        }
    }
}