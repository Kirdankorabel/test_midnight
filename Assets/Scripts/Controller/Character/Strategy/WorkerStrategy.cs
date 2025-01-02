using Controller.Building;
using Model;
using System.Collections.Generic;
using View;

namespace Controller.Characters
{
    public class WorkerStrategy : CharacterStrategy
    {
        private WorkerModel _model;
        private WorkController _workController;
        private RouteController _routeController;
        private GameLibrary _gameLibrary;

        public WorkerStrategy SetModel(WorkerModel model)
        {
            _model = model;
            return this;
        }

        public override CharacterStrategy SetController(NPCConroller characterController)
        {
            _workController = GameContext.DIContainer.Resolve<WorkController>();
            _routeController = GameContext.DIContainer.Resolve<RouteController>();
            _gameLibrary = GameContext.DIContainer.Resolve<GameLibrary>();
            _workController.OnTaskAdded += OnTaskAddedHeandker;
            _characterController = characterController;
            _characterController.OnAllCommandEnded += OnCharacterCommandEndHeandler;
            return this;
        }

        public override void InitializeStartCommands()
        {
            var task = _workController.GetFreeTask(_model.WorkerId);
            if (task != null)
            {
                TryToCreateTask();
            }
            else
            {
                Wait();
            }
        }

        protected override void OnCharacterCommandEndHeandler()
        {
            var lastCommand = _characterController.GetLastCommand;
            if ((lastCommand == null || lastCommand is WaitNPCCommand) && TryToCreateTask())
            {
                _characterController.BreakAction();
                return;
            }
            Wait();
        }

        private void OnTaskAddedHeandker()
        {
            var lastCommand = _characterController.GetLastCommand;
            if ((lastCommand == null || lastCommand is WaitNPCCommand) && TryToCreateTask())
            {
                _characterController.BreakAction();
            }
        }

        private bool TryToCreateTask()
        {
            var task = _workController.GetFreeTask(_model.WorkerId);//TODO проходиться по всему
            if(task == null)
            {
                return false;
            }
            if (task.Type == TaskType.craft)
            {
                CreateCraftCommand(task);
            }
            else if(task.Type == TaskType.delivery)
            {
                CreateDeliveryCommand(task);
            }
            else if (task.Type == TaskType.loot)
            {
                CreateLootCommand(task);
            }
            return true;
        }

        private void CreateCraftCommand(GameTaskModel task)
        {
            var storagePoint = _routeController.GetRoute(RouteTags.storage).RoutePoints.Find(p => p.IsFree);
            var plantPoint = _routeController.GetRoute(RouteTags.plant).RoutePoints.Find(p => p.IsFree);


            _characterController.Model.SetTaskModel(task);
            task.SetWorker(_characterController.Model.Id).SetStatus(TaskStatus.InProgress);

            var recipe = _gameLibrary.RecipeDataContainer.GetItem(task.RecipeId);
            var product = new List<string>() { recipe.ProductId };

            _characterController
                .AddCommand(new CommandModel(CommandType.move).SetPointTag(RouteTags.storage))
                .AddCommand(new CommandModel(CommandType.removeItems).SetPointTag(RouteTags.storage).SetItems(recipe.Components))
                .AddCommand(new CommandModel(CommandType.occupy).SetPointTag(RouteTags.plant))
                .AddCommand(new CommandModel(CommandType.move).SetPointTag(RouteTags.plant))
                .AddCommand(new CommandModel(CommandType.addItems).SetPointTag(RouteTags.plant).SetItems(recipe.Components))
                .AddCommand(new CommandModel(CommandType.use))
                .AddCommand(new CommandModel(CommandType.removeItems).SetPointTag(RouteTags.plant).SetItems(new List<string>() { recipe.ProductId }))
                .AddCommand(new CommandModel(CommandType.dispose).SetPointTag(RouteTags.plant))
                .AddCommand(new CommandModel(CommandType.move).SetPointTag(RouteTags.storage))
                .AddCommand(new CommandModel(CommandType.addItems).SetPointTag(RouteTags.storage).SetItems(new List<string>() { recipe.ProductId }))
                .AddCommand(new CommandModel(CommandType.addExp).SetExp(2));
        }

        private void CreateDeliveryCommand(GameTaskModel task)
        {
            var rask = (RackController)GameContext.DIContainer.Resolve<WorkshopController>().GetBuilding(task.BuildingId);

            if (rask == null)
            {
                Wait();
                return;
            }

            _characterController.Model.SetTaskModel(task);
            task.SetWorker(_characterController.Model.Id).SetStatus(TaskStatus.InProgress);

            _characterController
                .AddCommand(new CommandModel(CommandType.move).SetPointTag(RouteTags.storage))
                .AddCommand(new CommandModel(CommandType.removeItems).SetPointTag(RouteTags.storage).SetItems(rask.GetTargetItems))
                .AddCommand(new CommandModel(CommandType.move).SetPointTag(RouteTags.rackWorker).SetBuildingId(rask.PlObjectId))
                .AddCommand(new CommandModel(CommandType.addItems).SetPointTag(RouteTags.rackWorker).SetBuildingId(rask.PlObjectId).SetItems(rask.GetTargetItems))
                .AddCommand(new CommandModel(CommandType.addExp).SetExp(1));
        }

        private void CreateLootCommand(GameTaskModel task)
        {
            task.SetWorker(_characterController.Model.Id).SetStatus(TaskStatus.InProgress);
            _characterController.Model.SetTaskModel(task);

            _characterController
                .AddCommand(new CommandModel(CommandType.move).SetPointTag(RouteTags.loot))
                .AddCommand(new CommandModel(CommandType.waite).SetTime(0.2f))
                .AddCommand(new CommandModel(CommandType.move).SetPointTag(RouteTags.storage))
                .AddCommand(new CommandModel(CommandType.loot))
                .AddCommand(new CommandModel(CommandType.addExp).SetExp(2));
        }

        private void Wait()
        {
            var waitingPoint = _routeController.GetRoute(RouteTags.waitingPlace).GetPoint();

            if (waitingPoint != null)
            {
                _characterController
                    .AddCommand(new CommandModel(CommandType.move).SetPointTag(RouteTags.waitingPlace))
                    .AddCommand(new CommandModel(CommandType.waite).SetTime(float.MaxValue));
                _characterController.Move();
            }
            else
            {
                _characterController
                    .AddCommand(new CommandModel(CommandType.waite).SetTime(float.MaxValue));
                _characterController.Move();
            }
        }
    }
}