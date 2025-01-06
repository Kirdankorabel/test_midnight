using Controller;
using Controller.Characters;
using System;
using UnityEngine;
using View.UI;

namespace View.Game.Characters
{
    public class CharacterView : MonoBehaviour
    {
        public static event Action<CharacterView> OnDialogOpened;

        [SerializeField] private CharacterMover _characterMover;
        [SerializeField] private ClickableObject _clickableObject;
        [SerializeField] private Animator _animator;

        private string _characterId;

        public event Action OnMoveEnded;
        public event Action<CharacterView> OnRealesed;
        public event Action OnTould;

        public Animator Animator => _animator;
        public CharacterMover Mover => _characterMover;
        public string DialogId { get; private set; }

        public CharacterView SetDialogID(string dialogId)
        {
            DialogId = dialogId;
            return this;
        }

        private void Realese()
        {
            OnRealesed?.Invoke(this);
        }

        public void OpenUI()
        {
            OnTould?.Invoke();
            if (DialogId != null)
            {
                OnDialogOpened?.Invoke(this);
            }
        }

        private void OnDisable()
        {
            OnTould = null;
            DialogId = null;
        }
    }
}