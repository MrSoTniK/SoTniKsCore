using Zenject;

namespace Core.Infrastructure.Installers.Controllers 
{
    public abstract class ControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindControllers();
        }

        protected abstract void BindControllers();
    }
}