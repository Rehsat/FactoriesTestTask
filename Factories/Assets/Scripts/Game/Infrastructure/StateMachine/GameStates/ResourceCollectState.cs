using System.Collections.Generic;
using Game.Core.Player;
using Game.Core.PlayerResourcess;
using Game.Core.PlayerResourcess.ResourcFactories;
using Game.Infrastructure.CurrentLevelData;
using Game.Services.Canvases;
using Game.Services.PlayerResources;
using Game.UI.PopUps;
using Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.StateMachine.GameStates
{
    public class ResourceCollectState : IGameState
    {
        private readonly IFactory<PlayerResource, ResourceCollectBuildingPresenter> _resourceCollectorsFactory;
        private readonly IFactory<HUDView> _hudViewFactory;
        private readonly ICanvasLayersProvider _canvasLayersProvider;
        private readonly IPopUpsSpawnService _popUpsSpawner;
        private readonly Dictionary<PlayerResource, ResourceCollectBuildingPresenter> _resourceCollectorPresenters;
        
        private readonly ICurrentLevelDataProvider _levelDataProvider;
        private readonly IFactory<PlayerRoot> _playerFactory;
        private readonly IFactory<ResourcesPopUpPresenter> _resourcesListPresenterFactory;

        private HUDView _levelButtons;
        private ResourcesPopUpPresenter _resourcesPopUpPresenter;

        public ResourceCollectState(
            ICurrentLevelDataProvider levelDataProvider,
            IFactory<PlayerRoot> playerFactory,
            IFactory<ResourcesPopUpPresenter> resourcesListPresenterFactory,
            IFactory<PlayerResource, ResourceCollectBuildingPresenter> resourceCollectorsFactory,
            ICanvasLayersProvider canvasLayersProvider,
            IPopUpsSpawnService popUpsSpawner)
        {
            _levelDataProvider = levelDataProvider;
            _playerFactory = playerFactory;
            _resourcesListPresenterFactory = resourcesListPresenterFactory;
            _resourceCollectorsFactory = resourceCollectorsFactory;
            _canvasLayersProvider = canvasLayersProvider;
            _popUpsSpawner = popUpsSpawner;
            _resourceCollectorPresenters = new Dictionary<PlayerResource, ResourceCollectBuildingPresenter>();
        }
        
        public void Enter()
        {
            _levelButtons = _levelDataProvider.CurrentLevelData.LevelButtons;
            _canvasLayersProvider.SetToCanvas(CanvasLayer.Hud, _levelButtons.transform);
            
            _playerFactory.Create().transform.position = 
                _levelDataProvider.CurrentLevelData.PlayerSpawnPosition.position;
            _resourcesPopUpPresenter = _resourcesListPresenterFactory.Create();
            
            _levelButtons.ResourcesPopUpButton.onClick.AddListener(_resourcesPopUpPresenter.ShowPopUp);
            _levelButtons.SettingsPopUpButton.onClick.AddListener((() => 
                _popUpsSpawner.SpawnPopUp(PopUpType.VolumeChange)));
                
                
            // заглушка для алгоритма спавна производящих зданий
            // вместо этого в полноценной игре может быть конфиг в SO 
            var counter = 1; 
            _levelDataProvider.CurrentLevelData.FactoriesSpawnPoints.ForEach(spawnPoint =>
            {
                var resource = (PlayerResource)counter;
                var presenter = _resourceCollectorsFactory.Create(resource);
                
                presenter.SetPosition(spawnPoint.position);
                presenter.SetCollectEnabled(true);
                
                _resourceCollectorPresenters.Add(resource, presenter);
                counter++;
            });
        }

        public void Exit()
        {
            _levelButtons.ResourcesPopUpButton.onClick.RemoveAllListeners();
            _levelButtons.SettingsPopUpButton.onClick.RemoveAllListeners();
        }

        public void SetStateMachine(GameStateMachine stateMachine)
        {
        }
    }
}