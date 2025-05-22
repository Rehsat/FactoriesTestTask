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
        
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int HappyDanceKey = Animator.StringToHash("happy-dance");

        public void Construct(IMovementObserver movementObserver)
        {
            _compositeDisposable = new CompositeDisposable();
            movementObserver.IsMoving
                .Subscribe(SetIsRunning)
                .AddTo(_compositeDisposable);
        }

        public void SetIsRunning(bool isRunning)
        {
            _animator.SetBool(IsRunningKey, isRunning);
        }

        private void OnDestroy()
        {
            _compositeDisposable?.Dispose();
        }

        public void PlayAnimation(PlayerAnimation playerAnimation)
        {
            if(playerAnimation == PlayerAnimation.None) return;
            _animator.SetTrigger(HappyDanceKey); // можно будет добавить поиск хеша по словарю при необходимости
        }
    }
}
