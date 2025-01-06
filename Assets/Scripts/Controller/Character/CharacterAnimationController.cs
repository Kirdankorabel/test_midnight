using UnityEngine;

namespace Controller.Characters
{
    public class CharacterAnimationController
    {
        private readonly string movingBoolName = "IsMoving";
        private readonly string workingBoolName = "IsWorking";
        private readonly string talkClipName = "talk";
        private readonly string lookClipName = "look";
        public readonly float talkingClipTime = 3.767f;
        public readonly float lookingClipTime = 4.767f;

        private Animator _animator;

        public CharacterAnimationController(Animator animator)
        {
            _animator = animator;
        }

        public void PlayMoveAnimation(bool value)
        {
            _animator.SetBool(movingBoolName, value);
        }

        public void PlayWorkingAnimation(bool value)
        {
            _animator.SetBool(workingBoolName, value);
        }

        public float PlayTalkingAnimation()
        {
            _animator.Play("Talk");
            return _animator.GetCurrentAnimatorClipInfo(0).Length;
        }

        public float PlayLookindAnimation()
        {
            _animator.Play("Look");
            return _animator.GetCurrentAnimatorClipInfo(0).Length;
        }
    }
}