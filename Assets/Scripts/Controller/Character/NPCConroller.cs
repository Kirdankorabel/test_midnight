using Core;
using Model;
using UnityEngine;
using View.Game.Characters;

namespace Controller.Characters
{
    public class NPCConroller
    {
        private CharacterView _character;
        private CharacterMover _characterMover;
        private NPCCommand _lastCommand;
        private ItemCollectionController _itemCollectionController;
        private CharacterModel _characterModel;
        private CharacterStrategy _characterStrategy;
        private CharacterAnimationController _animationController;

        public event System.Action OnStarted;
        public event System.Action OnAllCommandEnded;
        public event System.Action OnDialogStarted;
        public event System.Action<string> OnItemAdded;

        public NPCCommand GetLastCommand => _lastCommand;
        public CharacterMover CharacterMover => _characterMover;
        public CharacterModel Model => _characterModel;
        public ItemCollectionController ItemCollectionController => _itemCollectionController;
        public CharacterStrategy CharacterStrategy => _characterStrategy;
        public CharacterAnimationController AnimationController => _animationController;

        public NPCConroller(CharacterModel characterModel)
        {
            _characterModel = characterModel;
            _itemCollectionController = new ItemCollectionController(characterModel.ItemCollectionModel);
        }

        public NPCConroller SetView(CharacterView character)
        {
            _characterMover = character.Mover;
            _characterMover.Construct();
            _character = character;
            _animationController = new CharacterAnimationController(character.Animator);
            return this;
        }

        public NPCConroller SetStrategy(CharacterStrategy strategy)
        {
            _characterStrategy = strategy;
            return this;
        }

        public void Move()
        {
            StartNextCommand();
        }

        public void InitializeLoaded()
        {
            var command = CommandCreator.CreateCommand(_characterModel.LastCommand);
            command.SetController(this);
            command.OnActionEnded += StartNextCommand;
            command.OnActionBreaked += BreakeAction;
            _lastCommand = command;
            _lastCommand.StartAction();
        }

        public NPCConroller AddCommand(CommandModel command)
        {
            _characterModel.AddCommand(command);
            return this;
        }

        public void BreakAction()
        {
            StartNextCommand();
        }

        public void Dispose()
        {
            OnStarted = null;
            OnAllCommandEnded = null;
            OnDialogStarted = null;
            OnItemAdded = null;
            _characterMover.Destroy();
            _characterModel.Destroy();
        }

        public void UpdateModel()
        {
            _characterModel.Position = _characterMover.transform.position;
            _characterModel.Rotation = _characterMover.transform.eulerAngles;
        }

        private NPCCommand CreateLastCommand()
        {
            var command = CommandCreator.CreateCommand(_characterModel.GetCommand());
            command.SetController(this);
            command.OnActionEnded += StartNextCommand;
            command.OnActionBreaked += BreakeAction;
            return command;
        }

        private void StartNextCommand()
        {
            if (_lastCommand != null)
            {
                _lastCommand.Dispose();
            }
            if (_characterModel.CommandCount == 0)
            {
                _lastCommand = null;
                OnAllCommandEnded?.Invoke();
            }
            else
            {
                _lastCommand = CreateLastCommand();
                _lastCommand.StartAction();
            }
        }

        private void BreakeAction()
        {
            if(_characterModel.GameTaskModel != null)
            {
                GameLog.AddMassage($"Task: {_characterModel.GameTaskModel.Type} was failed");
                _characterModel.GameTaskModel.SetStatus(TaskStatus.Failed);
                _characterModel.SetTaskModel(null);
                _characterModel.ClearCommands();
            }
            _lastCommand = null;
            OnAllCommandEnded?.Invoke();
        }
    }
}