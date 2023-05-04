using Zenject;

namespace Core.Infrastructure.Installers.World 
{
    public class WorldsInfoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            WorldsInfo worldsInfo = new();
            Container.Bind<WorldsInfo>().FromInstance(worldsInfo).AsSingle();
        }
    }
}