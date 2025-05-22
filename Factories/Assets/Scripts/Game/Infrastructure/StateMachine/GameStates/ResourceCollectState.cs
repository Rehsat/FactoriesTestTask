using Game.Core.Player;
using Game.Infrastructure.CurrentLevelData;
using Infrastructure.StateMachine;
using Zenject;

namespace Game.Infrastructure.StateMachine.GameStates
{
    public class ResourceCollectState : IGameState
    {
        private readonly ICurrentLevelDataProvider _levelDataProvider;
        private readonly IFactory<PlayerRoot> _playerFactory;

        public ResourceCollectState(
            ICurrentLevelDataProvider levelDataProvider,
            IFactory<PlayerRoot> playerFactory)
        {
            _levelDataProvider = levelDataProvider;
            _playerFactory = playerFactory;
        }
        public void Enter()
        {
            _playerFactory.Create().transform.position = 
                _levelDataProvider.CurrentLevelData.PlayerSpawnPosition.position;
        }

        public void Exit()
        {
        }

        public void SetStateMachine(GameStateMachine stateMachine)
        {
        }
    }
}