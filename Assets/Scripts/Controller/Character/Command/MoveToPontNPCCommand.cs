using Core;
using UnityEngine;

namespace Controller.Characters
{
    public class MoveToPontNPCCommand : NPCCommand
    {
        [SerializeField] private Vector3 position;
        [SerializeField] private string pointTag;
        [SerializeField] private string buildingId;

        public Vector3 Position => position;

        public MoveToPontNPCCommand(string pointTag, string buildingId)
        {
            this.pointTag = pointTag;
            this.buildingId = buildingId;
        }

        public override void StartAction()
        {
            if(string.IsNullOrEmpty(pointTag))
            {
                _characterController.CharacterMover.MoveToPoint(position, EndAction);
            }
            else if(string.IsNullOrEmpty(buildingId)) 
            {
                var routePoint = GameContext.DIContainer.Resolve<RouteController>().GetRoute(pointTag).GetPoint();
                if (routePoint == null)
                {
                    GameLog.AddMassage($"{pointTag} - rout not founded");
                    Break();
                    return;
                }
                _characterController.CharacterMover.MoveToPoint(routePoint.position, EndAction);
            }
            else
            {
                var  routePoint = GameContext.DIContainer.Resolve<RouteController>().GetRoute(buildingId, pointTag).GetPoint();
                if (routePoint == null)
                {
                    GameLog.AddMassage($"{pointTag} - rout not founded");
                    Break();
                    return;
                }
                _characterController.CharacterMover.MoveToPoint(routePoint.position, EndAction);
            }
        }
    }
}