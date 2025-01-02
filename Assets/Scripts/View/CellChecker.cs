using DataContainer;
using Model.Items;
using System.Linq;

public static class CellChecker
{
    public static bool CheckCell(CellData cellData, ItemModel itemModel)
    {
        return CheckType(cellData, itemModel) && CheckTags(cellData, itemModel);
    }

    private static bool CheckType(CellData cellData, ItemModel itemModel)
    {
        if (cellData.TargetTypes.Count == 0)
        {
            return true;
        }
        if (cellData.TargetTypes.Any(t => t == itemModel.Data.ItemType))
        {
            return true;
        }
        return false;
    }

    private static bool CheckTags(CellData cellData, ItemModel itemModel)
    {
        if (cellData.TargetTags.Count == 0)
        {
            return true;
        }
        foreach (var tag1 in cellData.TargetTags)
        {
            foreach (var tag2 in itemModel.Data.Tags)
            {
                if (tag1.Equals(tag2))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
