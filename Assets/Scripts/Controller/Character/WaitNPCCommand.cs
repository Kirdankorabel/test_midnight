using UnityEngine;

namespace Controller.Characters
{
    public class WaitNPCCommand : NPCCommand
    {
        [SerializeField] private float waitTime;

        public float WaitTime => waitTime;

        public WaitNPCCommand() { }

        public WaitNPCCommand(float waitTime)
        {
            this.waitTime = waitTime;
        }

        public WaitNPCCommand SetTime(float waitTime)
        {
            this.waitTime = waitTime;
            return this;
        }

        public override void StartAction()
        {
            _characterController.CharacterMover.WaitToTime(waitTime, EndAction);
        }
    }
}