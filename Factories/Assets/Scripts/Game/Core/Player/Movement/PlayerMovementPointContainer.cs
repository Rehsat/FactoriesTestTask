using System;
using Game.Services.Input;
using Game.Services.RaycastService;
using UniRx;
using UnityEngine;

namespace Game.Core.Player.Movement
{
    public class PlayerMovementPointContainer : IWorldPointContainer, IDisposable
    {
        private ReactiveProperty<Vector3> _currentPointToMove;
        public IReadOnlyReactiveProperty<Vector3> CurrentPoint => _currentPointToMove;

        public PlayerMovementPointContainer()
        {
            _currentPointToMove = new ReactiveProperty<Vector3>();
        }
        public void SetNewPoint(Vector3 newPoint)
        {
            _currentPointToMove.Value = new Vector3(newPoint.x, 0, newPoint.z);
        }
        public void Dispose()
        {
            _currentPointToMove?.Dispose();
        }
    }
}