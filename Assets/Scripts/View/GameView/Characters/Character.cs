using Controller;
using Controller.Characters;
using System;
using UnityEngine;
using View.UI;

namespace View.Game.Characters
{
    public class Character : MonoBehaviour
    {
        public static event Action<Character> OnDialogOpened;

        [SerializeField] private CharacterMover _characterMover;
        [SerializeField] private ClickableObject _clickableObject;

        private NPCConroller _characterConroller;
        private string _characterId;


        public event System.Action OnMoveEnded;
        public event System.Action<Character> OnRealesed;
        public event System.Action OnTould;

        public string Name => _characterId;
        public string DialogId { get; private set; }

        public Character Initialize(NPCConroller characterController, Vector3 startPosition, string characterName)
        {
            _characterMover.Construct();
            transform.position = startPosition;
            _characterId = characterName;
            _characterConroller = characterController;
            characterController
                .SetMover(_characterMover)
                .SetItemCollection(new ItemCollectionController(new Model.Items.ItemCollectionModel("Character", 20)));
            characterController.CharacterStrategy.InitializeStartCommands();

            _characterConroller.OnDialogStarted += OpenUI;
            _characterConroller.Move();
            
            return this;
        }

        public Character SetDialogID(string dialogId)
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