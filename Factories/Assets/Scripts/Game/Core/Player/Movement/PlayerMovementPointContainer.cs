using UniRx;
using UnityEngine;

namespace Game.Core.Player.Movement
{
    public class PlayerMovementPointContainer : IWorldPointContainer
    {
        private ReactiveProperty<Vector3> _currentPointToMove;
        public IReadOnlyReactiveProperty<Vector3> CurrentPoint => _currentPointToMove;

        public PlayerMovementPointContainer()
        {
            _currentPointToMove = new ReactiveProperty<Vector3>(Vector3.left*3);
        }
    }
}