using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private Slot[] slots;

    public override void InstallBindings()
    {
        Container.Bind<Slot[]>().FromInstance(slots).AsSingle().NonLazy();
        Container.Bind<IInventory>().To<Inventory>().AsSingle().NonLazy();
    }
}