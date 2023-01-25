using Zenject;

namespace Core.Infrastructure.Installers.Componenets 
{
    public abstract class ComponentsInstaller : MonoInstaller
    {
        public override void InstallBindings() 
        {
            BindComponents();
        }

        protected abstract void BindComponents();      
    }
}