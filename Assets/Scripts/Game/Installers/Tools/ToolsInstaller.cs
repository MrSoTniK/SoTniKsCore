using Core.Tools;
using Zenject;

namespace Game.Installers.Tools 
{
    public class ToolsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Randomizer randomizer = new();
            Container.Bind<Randomizer>().FromInstance(randomizer).AsSingle();
        }
    }
}