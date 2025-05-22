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
           transform.
               DOJumpAnimation(_interactAnimationDurationSecondss);
        }
    }

    public enum InteractCallback
    {
        None = 0,
        ResourceCollected = 1
    }
}
