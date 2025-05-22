using System.Collections.Generic;
using Game.Core.Player;
using Game.Core.PlayerResourcess.ResourcFactories;
using Game.Infrastructure.CurrentLevelData;
using Game.Services.PlayerResources;
using Infrastructure.StateMachine;
using Zenject;

namespace Game.Infrastructure.StateMachine.GameStates
{
    public class ResourceCollectState : IGameState
    {
        private readonly IFactory<PlayerResource, ResourceCollectBuildingPresenter> _resourceCollectorsFactory;
        private readonly Dictionary<PlayerResource, ResourceCollectBuildingPresenter> _resourcePresenters;
        
        private readonly ICurrentLevelDataProvider _levelDataProvider;
        private readonly IFactory<PlayerRoot> _playerFactory;

        public ResourceCollectState(
            ICurrentLevelDataProvider levelDataProvider,
            IFactory<PlayerRoot> playerFactory,
            IFactory<PlayerResource, ResourceCollectBuildingPresenter> resourceCollectorsFactory)
        {
            _levelDataProvider = levelDataProvider;
            _playerFactory = playerFactory;
            _resourceCollectorsFactory = resourceCollectorsFactory;
            _resourcePresenters = new Dictionary<PlayerResource, ResourceCollectBuildingPresenter>();
        }
        
        public void Enter()
        {
            _playerFactory.Create().transform.position = 
                _levelDataProvider.CurrentLevelData.PlayerSpawnPosition.position;
            
            
            // в будущем можно сделать более крутой метод создания. Через конфиги например. 
            // Для полноценной игры это костыль, но для тестового думаю сойдет 
            var counter = 1; 
            _levelDataProvider.CurrentLevelData.FactoriesSpawnPoints.ForEach(spawnPoint =>
            {
                var resource = (PlayerResource)counter;
                var presenter = _resourceCollectorsFactory.Create(resource);
                
                presenter.SetPosition(spawnPoint.position);
                presenter.SetCollectEnabled(true);
                
                _resourcePresenters.Add(resource, presenter);
                counter++;
            });
        }

        public void Exit()
        {
        }

        public void SetStateMachine(GameStateMachine stateMachine)
        {
        }
    }
}