using System;
using Game.Core.Player.Animations;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Core.Player.Movement
{
    public class PlayerToPointMovement : MonoBehaviour, IMovementObserver, IStopableMovement
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private ReactiveProperty<bool> _isMoving;
        private CompositeDisposable _currentMoveDisposable;
        private CompositeDisposable _compositeDisposable;
        
        public IReadOnlyReactiveProperty<bool> IsMoving => _isMoving;
        
        public void Construct(IWorldPointContainer worldPointContainer)
        {
            _isMoving = new ReactiveProperty<bool>(false);
            _compositeDisposable = new CompositeDisposable();
            worldPointContainer.CurrentPoint
              //  .Skip(1)
                .Subscribe(SetPointToMove)
                .AddTo(_compositeDisposable);
        }

        public void SetPointToMove(Vector3 point)
        {
            Debug.Log(point);
            _currentMoveDisposable?.Dispose();
            _currentMoveDisposable = new CompositeDisposable();
            StartMoveToPoint(point);
        }

        private void StartMoveToPoint(Vector3 point)
        {
            _isMoving.Value = true;
            _navMeshAgent.destination = point;
            Observable.EveryUpdate()
                .Subscribe((l =>
                {
                    if (CheckIsReachedPoint(point))
                        Stop();
                }))
                .AddTo(_currentMoveDisposable);
        }

        private bool CheckIsReachedPoint(Vector3 point)
        {
            return (transform.position - point).magnitude < 0.1f;
        }
        [ContextMenu("Stop")]
        public void Stop()
        {
            _isMoving.Value = false;
            _navMeshAgent.destination = transform.position;
            _currentMoveDisposable?.Dispose();
        }

        private void OnDestroy()
        {
            _compositeDisposable?.Dispose();
            _currentMoveDisposable?.Dispose();
        }
    }
}