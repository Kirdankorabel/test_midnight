using Core;
using UnityEngine;

namespace Controller.Characters
{
    public class MoveToPontNPCCommand : NPCCommand
    {
        [SerializeField] private string pointTag;
        [SerializeField] private string buildingId;

        public MoveToPontNPCCommand(string pointTag, string buildingId)
        {
            this.pointTag = pointTag;
            this.buildingId = buildingId;
        }

        public override void StartAction()
        {
            RoutePoint routePoint;
            if(string.IsNullOrEmpty(buildingId)) 
            {
                routePoint = GameContext.DIContainer.Resolve<RouteController>().GetRoute(pointTag).GetPoint();
                if (routePoint == null)
                {
                    GameLog.AddMassage($"{pointTag} - rout not founded");
                    Break();
                    return;
                }
            }
            else
            {
                routePoint = GameContext.DIContainer.Resolve<RouteController>().GetRoute(buildingId, pointTag).GetPoint();
                if (routePoint == null)
                {
                    GameLog.AddMassage($"{pointTag} - rout not founded");
                    Break();
                    return;
                }
            }
            _characterController.CharacterMover.MoveToPoint(routePoint.position, EndAction);
            _characterController.AnimationController.PlayMoveAnimation(true);
        }

        public override void EndAction()
        {
            _characterController.AnimationController.PlayMoveAnimation(false);
            base.EndAction();
        }
    }
}