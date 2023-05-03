using Core.Infrastructure;
using Zenject;

public class WorldsInfoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        WorldsInfo worldsInfo = new();
        Container.Bind<WorldsInfo>().FromInstance(worldsInfo).AsSingle();
    }
}
