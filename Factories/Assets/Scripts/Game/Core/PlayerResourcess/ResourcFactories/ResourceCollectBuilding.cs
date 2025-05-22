using System;
using DG.Tweening;
using EasyFramework.ReactiveTriggers;
using Game.Core.Interactables;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

namespace Game.Core.PlayerResourcess.ResourcFactories
{
    public class ResourceCollectBuilding : MonoBehaviour, IResourceCollectBuilding
    {
        [SerializeField] private Transform _modelRoot;
        [SerializeField] private InteractableView _interactableView;
        [SerializeField] private ResourceUiView _resourceUiView;
        [SerializeField] private InteractableTrigger _interactableTrigger;

        private CompositeDisposable _compositeDisposable;
        private ReactiveTrigger _onResourceCollected;

        public IReadOnlyReactiveTrigger OnCollectResource => _onResourceCollected;
        
        public void Construct()
        {
            _compositeDisposable = new CompositeDisposable();
            _onResourceCollected = new ReactiveTrigger();

            Initialize();
        }

        private void Initialize()
        {
            _interactableTrigger
                .Construct(InteractCallback.ResourceCollected);
            
            _interactableTrigger.OnInteracted
                .Subscribe((() =>
                {
                    _onResourceCollected.Notify();
                    _interactableTrigger.SetEnabled(false);
                }))
                .AddTo(_compositeDisposable);

            _interactableView.OnInteract
                .SubscribeWithSkip((() => _interactableTrigger.SetEnabled(true)))
                .AddTo(_compositeDisposable);
        }

        public void SetModel(GameObject model)
        {
            model.transform.SetParent(_modelRoot);
            model.transform.localPosition = Vector3.zero;
        }

        public void SetSprite(Sprite sprite)
        {
            _resourceUiView.SetSprite(sprite);
        }
        
        public void SetTitle(string title)
        {
            _resourceUiView.SetTitle(title);
        }

        public void SetResourceViewCount(float resourceCount)
        {
            _resourceUiView.SetResourceCount(resourceCount);
            
            _resourceUiView.transform.DOKill();
            _resourceUiView.transform.DOJumpAnimation(0.3f);
        }

        private void OnDestroy()
        {
            _onResourceCollected?.Dispose();
            _compositeDisposable?.Dispose();
        }
    }
}
