using Core.Infrastructure.Installers.DataBases;

namespace Game.Installers.DataBases 
{
    public class DataBasesProjectInstaller : DataBasesInstaller
    {
	//Example
	//  [SerializeField] private LevelsDataBase _levelsDataBase;     

        protected override void BindDBs()
        {
	    //Example
	    //Container.Bind<LevelsDataBase>().FromInstance(_levelsDataBase);
            
        }
    }
}