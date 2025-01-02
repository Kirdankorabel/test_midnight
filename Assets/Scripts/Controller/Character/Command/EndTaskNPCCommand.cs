using Controller.Building;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Characters
{
    public class EndTaskNPCCommand : NPCCommand
    {
        private GameTaskModel _gameTask;
        private WorkersController _workersController;
        private int _exp;

        public EndTaskNPCCommand(int exp)
        {
            _exp = exp;
        }

        public override NPCCommand SetController(NPCConroller characterMover)
        {
            _gameTask = characterMover.Model.GameTaskModel;
            return base.SetController(characterMover);
        }

        public override void StartAction()
        {
            _gameTask.SetStatus(TaskStatus.Success);
            GameContext.DIContainer.Resolve<WorkshopController>().WorkersController
                .AddExpForWorker(_exp, _characterController.Model.Id);

            EndAction();
        }
    }
}