using System;
using EasyFramework.ReactiveTriggers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Core.Interactables
{
    public class InteractableTrigger : MonoBehaviour
    {
        [SerializeField] private Collider _trigger;
        
        private CompositeDisposable _compositeDisposable;
        private ReactiveTrigger _onInteracted;
        
        public IReadOnlyReactiveTrigger OnInteracted => _onInteracted;

        public void Construct(InteractCallback callbackOnTrigger)
        {
            _compositeDisposable = new CompositeDisposable();
            _onInteracted = new ReactiveTrigger();
            
            _trigger.isTrigger = true;   
            _trigger
                .OnTriggerEnterAsObservable()
                .Subscribe((enteredCollider =>
            {
                if (enteredCollider.gameObject.TryGetComponent<IInteractCallbackReciever>(out var callbackReciever))
                {
                    _onInteracted.Notify();
                    callbackReciever.SendCallback(callbackOnTrigger);
                }
            }))
                .AddTo(_compositeDisposable);
        }

        public void SetEnabled(bool isEnabled)
        {
            Debug.LogError(isEnabled);
            _trigger.gameObject.SetActive(isEnabled);
        }

        private void OnDestroy()
        {
            _compositeDisposable?.Dispose();
            _onInteracted?.Dispose();
        }
    }
}