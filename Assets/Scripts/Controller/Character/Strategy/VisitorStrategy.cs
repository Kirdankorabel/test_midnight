using System.Collections.Generic;
using UnityEngine;

namespace Controller.Characters
{
    public class VisitorStrategy : CharacterStrategy
    {
        private CharacterController _controller;
        private RouteController _routeController;
        private ShopingManager _shopingManager;
        private List<string> _targetItems;
        private float _chance = 0.2f;

        public override void InitializeStartCommands()
        {
            var route = _routeController.GetRoute(RouteTags.rackVisitor);
            if (route == null)
            {
                MoveToStallAndReturn();
                return;
            }
            else
            {
                MoveToRack();
            }
        }

        public override CharacterStrategy SetController(NPCConroller characterController)
        {
            _routeController = GameContext.DIContainer.Resolve<RouteController>();
            _shopingManager = GameContext.DIContainer.Resolve<ShopingManager>();
            _characterController = characterController;
            _characterController.OnAllCommandEnded += OnCharacterCommandEndHeandler;
            return this;
        }

        public VisitorStrategy SetChance(float value)
        {
            _chance = value;
            return this;
        }

        public VisitorStrategy SetTargetItems(List<string> itemIds)
        {
            _targetItems = itemIds;
            return this;
        }

        protected override void OnCharacterCommandEndHeandler()
        {
            MoveToExit();
            _characterController.Move();
        }

        private void MoveToRack()
        {
            var rackPoint = _routeController.GetRoute(RouteTags.rackVisitor).GetPoint();
            var targetItems = _shopingManager.GetRandomItems(1);

            if(rackPoint == null)
            {
                MoveToStallAndReturn();
                return;
            }

            _characterController
                .AddCommand(new Model.CommandModel(CommandType.move).SetPointTag(RouteTags.rackVisitor))
                .AddCommand(new Model.CommandModel(CommandType.shoping).SetPointTag(RouteTags.rackVisitor).SetItems(targetItems))
                .AddCommand(new Model.CommandModel(CommandType.pay));
        }

        private void MoveToStallAndReturn()
        {
            var targetItems = _shopingManager.GetRandomItems(1);

            _characterController
                .AddCommand(new Model.CommandModel(CommandType.move).SetPointTag(RouteTags.stallVisitor))
                .AddCommand(new Model.CommandModel(CommandType.shoping).SetPointTag(RouteTags.storage).SetItems(targetItems))
                .AddCommand(new Model.CommandModel(CommandType.pay));
        }

        private void MoveToExit()
        {
            _characterController
                .AddCommand(new Model.CommandModel(CommandType.move).SetPointTag(RouteTags.exit))
                .AddCommand(new Model.CommandModel(CommandType.destroy));
        }
    }
}