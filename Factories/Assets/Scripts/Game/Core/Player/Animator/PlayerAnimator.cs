using System;
using Game.Core.Player.Movement;
using UnityEngine;
using UniRx;

namespace Game.Core.Player.Animations
{
    public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
    {
        [SerializeField] private Animator _animator;

        private CompositeDisposable _compositeDisposable;
        
        private static readonly int IsRunning = Animator.StringToHash("is-running");

        public void Construct(IMovementObserver movementObserver)
        {
            _compositeDisposable = new CompositeDisposable();
            movementObserver.IsMoving
                .Subscribe(SetIsRunning)
                .AddTo(_compositeDisposable);
        }

        public void SetIsRunning(bool isRunning)
        {
            _animator.SetBool(IsRunning, isRunning);
        }

        private void OnDestroy()
        {
            _compositeDisposable?.Dispose();
        }
    }
}
