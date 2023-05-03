using Core.Infrastructure.Installers.Systems;

namespace Game.Installers.Systems 
{
    public class GameSystemsInstaller : SystemsInstaller
    {
        protected override void AddPreInitSystems()
        {
	    //Example:
            //var firstControlPointPreInitSystem = Container.Instantiate<FirstControlPointPreInitSystem>();
            //Container.Bind<FirstControlPointPreInitSystem>().FromInstance(firstControlPointPreInitSystem);
            //EcsPreInitSystems.Add(firstControlPointPreInitSystem);
        }

        protected override void AddInitSystems()
        {
            //Example:
            //var inputsInitSystem = Container.Instantiate<InputsInitSystem>();
            //Container.Bind<InputsInitSystem>().FromInstance(inputsInitSystem);
            //EcsInitSystems.Add(inputsInitSystem);       
        }

        protected override void AddRunSystems()
        {
	    //Example
            //var controlPointsResolvingRunSystem = Container.Instantiate<ControlPointsResolvingRunSystem>();
            //Container.Bind<ControlPointsResolvingRunSystem>().FromInstance(controlPointsResolvingRunSystem);
            //EcsRunSystems.Add(controlPointsResolvingRunSystem);        
        }

        protected override void AddFixedRunSystems()
        {
            //Example
            //var characterMovementRunSystem = Container.Instantiate<CharacterMovementRunSystem>();
            //Container.Bind<CharacterMovementRunSystem>().FromInstance(characterMovementRunSystem);
            //EcsFixedRunSystems.Add(characterMovementRunSystem);
        }
    }
}