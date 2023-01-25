using Zenject;

namespace Core.Infrastructure.Installers.DataBases 
{
    public abstract class DataBasesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindDBs();
        }

        protected abstract void BindDBs();      
    }
}