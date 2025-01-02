using Controller;
using DI;
using UnityEngine;
using View.UI;

public class AccountControllerInstaller : Installer
{
    [SerializeField] private BalanceView _balanceView;
    [SerializeField] private ReseacrhItemCollectionView _reseacrhItemCollectionView;

    public override void Install(DIContainer dIContainer)
    {
        dIContainer.RegisterInstance(
            new AccountController()
            .SetBalanceView(_balanceView)
            .SetResearhView(_reseacrhItemCollectionView));
    }
}
