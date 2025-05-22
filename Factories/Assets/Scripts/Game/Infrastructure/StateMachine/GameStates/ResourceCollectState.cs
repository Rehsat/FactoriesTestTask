using System.Collections.Generic;
using Game.Core.Player;
using Game.Core.PlayerResourcess;
using Game.Core.PlayerResourcess.ResourcFactories;
using Game.Infrastructure.CurrentLevelData;
using Game.Services.Canvases;
using Game.Services.PlayerResources;
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
        private readonly Dictionary<PlayerResource, ResourceCollectBuildingPresenter> _resourceCollectorPresenters;
        
        private readonly ICurrentLevelDataProvider _levelDataProvider;
        private readonly IFactory<PlayerRoot> _playerFactory;
        private readonly IFactory<ResourcesPopUpPresenter> _resourcesListPresenterFactory;

        private ResourcesPopUpPresenter _resourcesPopUpPresenter;

        public ResourceCollectState(
            ICurrentLevelDataProvider levelDataProvider,
            IFactory<PlayerRoot> playerFactory,
            IFactory<ResourcesPopUpPresenter> resourcesListPresenterFactory,
            IFactory<PlayerResource, ResourceCollectBuildingPresenter> resourceCollectorsFactory,
            IFactory<HUDView> hudViewFactory,
            ICanvasLayersProvider canvasLayersProvider)
        {
            _levelDataProvider = levelDataProvider;
            _playerFactory = playerFactory;
            _resourcesListPresenterFactory = resourcesListPresenterFactory;
            _resourceCollectorsFactory = resourceCollectorsFactory;
            _hudViewFactory = hudViewFactory;
            _canvasLayersProvider = canvasLayersProvider;
            _resourceCollectorPresenters = new Dictionary<PlayerResource, ResourceCollectBuildingPresenter>();
        }
        
        public void Enter()
        {
            var hud = _hudViewFactory.Create();
            _canvasLayersProvider.SetToCanvas(CanvasLayer.Hud, hud.transform);
            
            _playerFactory.Create().transform.position = 
                _levelDataProvider.CurrentLevelData.PlayerSpawnPosition.position;
            _resourcesPopUpPresenter = _resourcesListPresenterFactory.Create();
            
            hud.ResourcesPopUpButton.onClick.AddListener(_resourcesPopUpPresenter.ShowPopUp);
                
                
            // заглушка для алгоритма спавна производящих зданий
            // вместо этого в полноценной игре может быть конфиг в SO 
            var counter = 1; 
            _levelDataProvider.CurrentLevelData.FactoriesSpawnPoints.ForEach(spawnPoint =>
            {
                Debug.LogError(counter);
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
            _levelDataProvider.CurrentLevelData.HudView.
                ResourcesPopUpButton.gameObject.SetActive(false);
        }

        public void SetStateMachine(GameStateMachine stateMachine)
        {
        }
    }
}