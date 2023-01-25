using Zenject;

namespace Core.Infrastructure.Installers.Factories 
{
    public abstract class FactoriesSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
        }

        protected virtual void BindFactories(){ }
    }
}