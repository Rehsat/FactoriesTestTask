using DG.Tweening;
using EasyFramework.ReactiveTriggers;
using UnityEngine;

namespace Game.Core.Interactables
{
    public class InteractableView : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _interactAnimationDurationSecondss = 0.3f;
        private ReactiveTrigger _onInteract = new ReactiveTrigger();
        public IReadOnlyReactiveTrigger OnInteract => _onInteract;

        public void Interact()
        {
            Debug.LogError(123);
            PlayInteractAnimation();
            _onInteract.Notify();
        }

        private void PlayInteractAnimation()
        {
           transform.DOKill();
           var startScale = transform.localScale;
           transform
               .DOScale(transform.localScale * 1.2f, _interactAnimationDurationSecondss)
               .SetEase(Ease.OutCubic)
               .SetLoops(2, LoopType.Yoyo)
               .OnKill(() => transform.localScale = startScale);
        }
    }

    public enum InteractCallback
    {
        None = 0,
        ResourceCollected = 1
    }
}
