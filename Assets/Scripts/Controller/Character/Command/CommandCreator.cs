using Model;

namespace Controller.Characters
{
    public static class CommandCreator
    {
        public static NPCCommand CreateCommand(CommandModel command)
        {
            switch (command.Type)
            {
                case CommandType.waite:
                    return new WaitNPCCommand(command.Time);
                case CommandType.move:
                    return new MoveToPontNPCCommand(command.PointTag, command.BuildingId);
                case CommandType.addItems:
                    return new AddItemToCollectionCommand(command.PointTag, command.BuildingId, command.ItemIds);
                case CommandType.removeItems:
                    return new RemoveItemFromCollectionCommand(command.PointTag, command.BuildingId, command.ItemIds);
                case CommandType.loot:
                    return new CreateLootNPCCommand(command.Time);
                case CommandType.use:
                    return new UseBuildingNPCCommand();
                case CommandType.occupy:
                    return new OccupyBuildingNPCCommand(command.PointTag);
                case CommandType.dispose:
                    return new DisposeBuildingNPCCommand(command.PointTag);
                case CommandType.addExp:
                    return new EndTaskNPCCommand(command.Exp);
                case CommandType.shoping:
                    return new MakePlannedPurchaseCommand(command.PointTag, command.ItemIds);
                case CommandType.destroy:
                    return new DestroyNPCCommand();
                case CommandType.pay:
                    return new PayForItemsNPCCommand();
                default:
                    UnityEngine.Debug.LogError(command.Type); 
                    return new WaitNPCCommand(float.MaxValue);
            }
        }
    }
}