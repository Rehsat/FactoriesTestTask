using Game.Core.Player;
using Game.Core.Player.Movement;
using Game.Infrastructure.AssetsManagement;
using UnityEngine;
using Zenject;

namespace Game.Factories
{
    public class PlayerFactory : IFactory<PlayerRoot>
    {
        private readonly IWorldPointContainer _playerPointToMove;
        
        private PlayerRoot _playerPrefab;

        public PlayerFactory(IPrefabsProvider prefabProvider, 
            IWorldPointContainer playerPointToMove)
        {
            _playerPointToMove = playerPointToMove;
            _playerPrefab = prefabProvider.GetPrefabsComponent<PlayerRoot>(Prefab.Player);
        }
        public PlayerRoot Create()
        {
            var player = Object.Instantiate(_playerPrefab);
            player.Construct(_playerPointToMove);
            return player;
        }
    }
}
