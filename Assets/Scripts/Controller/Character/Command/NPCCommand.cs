using UnityEngine;

namespace Controller.Characters
{
    public abstract class NPCCommand
    {
        protected NPCConroller _characterController;

        public event System.Action OnActionEnded;
        public event System.Action OnActionBreaked;

        public virtual NPCCommand SetController(NPCConroller characterMover)
        {
            _characterController = characterMover;
            return this;
        }

        public abstract void StartAction();

        public virtual void EndAction()
        {
            OnActionEnded?.Invoke();
        }

        protected virtual void Break()
        {
            OnActionBreaked?.Invoke();
        }

        public virtual void Dispose()
        {
            OnActionEnded = null;
            OnActionBreaked = null;
        }
    }
}