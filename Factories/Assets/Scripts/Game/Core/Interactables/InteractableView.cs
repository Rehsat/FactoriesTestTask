using DG.Tweening;
using EasyFramework.ReactiveTriggers;
using UnityEngine;

namespace Game.Core.Interactables
{
    public class InteractableView : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _interactAnimationDurationSecondss = 0.3f;
        private Sequence _animationSequence;

        private Vector3 _startScale;
        private Vector3 _startPosition;
        
        private Tween _jumpMoveTween;
        private Tween _jumpTween;
        private Tween _returnScaleTween;
        private Tween _returnPositionTween;
        
        private ReactiveTrigger _onInteract = new ReactiveTrigger();
        public IReadOnlyReactiveTrigger OnInteract => _onInteract;

        public void Interact()
        {
            PlayInteractAnimation();
            _onInteract.Notify();
        }

        private void PlayInteractAnimation()
        {
           /* if(_jumpTween == null)
                InitTweens();
            
            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence();

            _animationSequence
                .Append(_jumpTween)
                .Join(_jumpMoveTween)
                .Append(_returnScaleTween)
                .Join(_returnPositionTween)
                .OnKill((() =>
                {
                    transform.localScale = _startScale;
                    transform.localPosition = _startPosition;
                }));
            
            _animationSequence.Play();*/
           transform.DOKill();
           var startScale = transform.localScale;
           transform
               .DOScale(transform.localScale * 1.2f, _interactAnimationDurationSecondss)
               .SetEase(Ease.OutCubic)
               .SetLoops(2, LoopType.Yoyo)
               .OnKill(() => transform.localScale = startScale);
        }

        /*private void InitTweens()
        {
            _startScale = transform.lossyScale;
            _startPosition = transform.localPosition;
            var jumpScale = _startScale.y * 1.5f;
            var jumpMovement = (jumpScale - _startScale.y) / 2f;
            
            var jumpSeconds = _interactAnimationDurationSecondss / 3f;
            var returnSeconds = _interactAnimationDurationSecondss / 2f;
            
            var jumpEase = Ease.OutCubic;
            var returnEase = Ease.InCubic;
            
            _jumpMoveTween = transform
                .DOLocalMoveY(transform.position.y + jumpMovement, jumpMovement)
                .SetEase(jumpEase);
            _jumpTween = transform
                .DOScaleY(jumpScale, jumpSeconds)
                .SetEase(jumpEase);

            _returnPositionTween = transform
                .DOLocalMove(_startPosition, returnSeconds)
                .SetEase(returnEase);
            _returnScaleTween = transform
                .DOScale(returnSeconds, returnSeconds)
                .SetEase(returnEase);
        }*/
    }
}
