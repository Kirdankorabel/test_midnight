using UnityEngine;

namespace Controller.Characters
{
    public class MoveToTargetNPCCommand : NPCCommand
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private string _targetId;

        public Transform TargetTransform => _targetTransform;

        public MoveToTargetNPCCommand() { }

        public MoveToTargetNPCCommand SetTransform(Transform transform, string targetId)
        {
            _targetTransform = transform;
            _targetId = targetId;
            return this;
        }

        public override void StartAction()
        {
            _characterController.CharacterMover.MoveToTarget(_targetTransform, EndAction);
        }
    }
}