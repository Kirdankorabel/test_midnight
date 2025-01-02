using UnityEngine;

namespace Controller.Characters
{
    public abstract class CharacterStrategy
    {
        protected NPCConroller _characterController;

        public abstract CharacterStrategy SetController(NPCConroller characterController);
        public abstract void InitializeStartCommands();
        protected abstract void OnCharacterCommandEndHeandler();
    }
}