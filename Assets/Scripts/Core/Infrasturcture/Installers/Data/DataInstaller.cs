using Zenject;

namespace Core.Infrastructure.Installers.Data 
{
    public abstract class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindData();
        }

        protected abstract void BindData();       
    }
}