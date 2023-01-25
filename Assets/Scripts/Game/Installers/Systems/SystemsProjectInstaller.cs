using Core.Infrastructure.Installers.Systems;
using Core.Views;

namespace Game.Installers.Systems 
{
    public class SystemsProjectInstaller : SystemsInstaller
    {
        protected override void AddPreInitSystems()
        {
           
        }

        protected override void AddInitSystems()
        {
            
        }

        protected override void AddRunSystems()
        {
            var viewsEntityInitializingSystem = Container.Instantiate<ViewsEntityInitializingSystem>();
            Container.Bind<ViewsEntityInitializingSystem>().FromInstance(viewsEntityInitializingSystem);
            EcsRunSystems.Add(viewsEntityInitializingSystem);
        }

        protected override void AddFixedRunSystems()
        {
            
        }
    }
}