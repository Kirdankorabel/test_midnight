using DataContainer;
using DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;

public class GameLiblaryInstaller : Installer
{
    [SerializeField] private List<ItemDataContainer> _itemDatas;
    [SerializeField] private AlcemicalPlantDataContainer _alcemicalPlantDataContainer;
    [SerializeField] private PlacableObjectDataContainer _placableObjectDataContainer;
    [SerializeField] private RecipeDataContainer _recipeDataContainer;

    public override void Install(DIContainer dIContainer)
    {
        var liblary = new GameLibrary();
        liblary.SetAlcemicalPlantDataContainer(_alcemicalPlantDataContainer)
               .SetPlacableObjectDataContainer(_placableObjectDataContainer)
               .SetItemDatas(_itemDatas)
               .SetRecipes(_recipeDataContainer);
        dIContainer.RegisterInstance(liblary);
    }
}
