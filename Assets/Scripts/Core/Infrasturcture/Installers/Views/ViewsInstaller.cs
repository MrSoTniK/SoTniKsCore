using Zenject;

namespace Core.Infrastructure.Installers.Views 
{
    public abstract class ViewsInstaller : MonoInstaller
    {
        public override void InstallBindings() 
        {
            BindViews();
        }

        protected abstract void BindViews();
    }
}