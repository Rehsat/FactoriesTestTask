using Game.Core.Player;
using Game.Core.Player.Movement;
using Game.Core.PlayerResourcess.ResourcFactories;
using Game.Factories;
using Game.Infrastructure;
using Game.Infrastructure.AssetsManagement;
using Game.Infrastructure.Configs;
using Game.Infrastructure.CurrentLevelData;
using Game.Infrastructure.StateMachine.GameStates;
using Game.Services.Cameras;
using Game.Services.Canvases;
using Game.Services.Input;
using Game.Services.PlayerResources;
using Game.Services.RaycastService;
using Game.UI.PopUps;
using Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ProjectInstaller : MonoInstaller
    { 
        [SerializeField] private GameConfig _config;
        [SerializeField] private CoroutineStarter _coroutineStarter;
        [SerializeField] private CameraService _cameraService;
        [SerializeField] private CanvasLayersProvider canvasLayersProvider;
        [SerializeField] private PopUpsSpawner _popUpsSpawner;
        public override void InstallBindings()
        {
            InstallConfig(_config);
            InstallInfrastructure();
            InstallFactories();
            InstallServices();
            InstallPlayer();
            InstallStateMachine();
        }
        private void InstallConfig(IGameConfig gameConfig)
        {
            Container.Bind<IPrefabsProvider>().FromInstance(gameConfig.PrefabsProvider).AsSingle();
            Container.Bind<ISpriteByResourceTypeContainer>().FromInstance(gameConfig.SpriteByResourceTypeContainer).AsSingle();
        }
        
        private void InstallInfrastructure()
        {
            Container.Bind<ICoroutineStarter>().To<CoroutineStarter>().FromInstance(_coroutineStarter).AsSingle();
            
            Container.Bind<ICurrentLevelDataProvider>().To<CurrentLevelDataProvider>().FromNew().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().FromNew().AsSingle();
        }

        private void InstallFactories()
        {
            Container.Bind<IFactory<PlayerRoot>>().To<PlayerFactory>().FromNew().AsSingle();
            Container.Bind<IFactory<Canvas>>().To<CanvasFactory>().FromNew().AsSingle();
            
            Container.Bind<IFactory<PlayerResource, IResourceCollectBuilding>>()
                .To<ResourceViewFactory>().FromNew().AsSingle();
            Container.Bind<IFactory<PlayerResource, ResourceCollectBuildingPresenter>>()
                .To<CollectBuildingPresenterFactory>().FromNew().AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<ICanvasLayersProvider>().To<CanvasLayersProvider>().FromInstance(canvasLayersProvider).AsSingle();
            Container.Bind<ICameraService>().To<CameraService>().FromInstance(_cameraService).AsSingle();
            Container.Bind<IPopUpsSpawnService>().To<PopUpsSpawner>().FromInstance(_popUpsSpawner).AsSingle();

            Container.Bind<IPlayerResourcesService>().To<PlayerResourcesService>().FromNew().AsSingle();
            Container.Bind<IRaycastService>().To<RaycastService>().FromNew().AsSingle();
            Container.Bind<IInputService>().To<InputService>().FromNew().AsSingle();
        }

        private void InstallPlayer()
        {
            Container.Bind<IWorldPointContainer>().To<PlayerMovementPointContainer>().FromNew().AsSingle();
            Container.Bind<RaycastInteracter>().FromNew().AsSingle().NonLazy();
        }

        private void InstallStateMachine()
        {
            Container.Bind<IGameState>().To<BootstrapGameState>().FromNew().AsSingle();
            Container.Bind<IGameState>().To<ResourceCollectState>().FromNew().AsSingle();
            Container.Bind<GameStateMachine>().FromNew().AsSingle();
        }
    }
}