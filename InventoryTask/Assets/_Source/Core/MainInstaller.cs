using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private DebugMenuData debugMenuData;

    public override void InstallBindings()
    {
        Container.Bind<InventoryView>().FromInstance(inventoryView).AsSingle().NonLazy();
        Container.Bind<DebugMenuData>().FromInstance(debugMenuData).AsSingle().NonLazy();

        Container.Bind<IInventory>().To<Inventory>().AsSingle().NonLazy();
        Container.Bind<DebugMenu>().AsSingle().NonLazy();
    }
}