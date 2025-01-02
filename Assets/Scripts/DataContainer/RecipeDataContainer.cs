using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataContainer
{
    [CreateAssetMenu(fileName = "RecipeDataContainer", menuName = "ScriptableObjects/DataContainer/RecipeDataContainer", order = 1)]
    public class RecipeDataContainer : DataContainer<RecipeData>
    {
        public RecipeData GetRecipe(List<string> items)
        {
            return Data.Find(r => CompareRecipe(r, items));
        }

        public bool CompareRecipe(RecipeData recipeData, List<string> itemIds)
        {
            var components = recipeData.Components;
            components.Sort();
            itemIds.Sort();

            return itemIds.SequenceEqual(components);
        }
    }
}