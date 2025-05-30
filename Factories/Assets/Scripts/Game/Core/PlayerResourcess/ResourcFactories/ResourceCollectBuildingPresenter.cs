﻿using System;
using Game.Services.PlayerResources;
using UniRx;
using UnityEngine;

namespace Game.Core.PlayerResourcess.ResourcFactories
{
    public class ResourceCollectBuildingPresenter : IDisposable
    {
        private readonly PlayerResourceModel _resourceModel;
        private readonly IResourceCollectBuilding _resourceView;
        private readonly IPlayerResourcesService _resourcesService;
        private readonly CompositeDisposable _compositeDisposable;
        private readonly ReactiveProperty<bool> _isEnabled;

        private CompositeDisposable _collectDisposable;
        public ResourceCollectBuildingPresenter(
            PlayerResourceModel resourceModel
            ,IResourceCollectBuilding resourceView
            ,IPlayerResourcesService resourcesService)
        {
            _resourceModel = resourceModel;
            _resourceView = resourceView;
            _resourcesService = resourcesService;
            
            _compositeDisposable = new CompositeDisposable();
            _isEnabled = new ReactiveProperty<bool>();
            

            Initialize();
        }

        private void Initialize()
        {
            _isEnabled
                .Subscribe(HandleEnabledStateChange)
                .AddTo(_compositeDisposable);

            _resourceView.SetSprite(_resourceModel.Sprite);
            _resourceView.SetTitle(_resourceModel.ResourceType.ToString());
            
            _resourceModel.ResourceCount
                .Subscribe(newResourceValue =>
                    _resourceView.SetResourceViewCount(newResourceValue))
                .AddTo(_compositeDisposable);

            _resourceView.OnCollectResource
                .SubscribeWithSkip(CollectResources)
                .AddTo(_compositeDisposable);
        }

        public void SetCollectEnabled(bool isEnabled)
        {
            _isEnabled.Value = isEnabled;
        }
        
        public void SetPosition(Vector3 position)
        {
            if (_resourceView is MonoBehaviour resourceView)
                resourceView.transform.position = position;
        }

        private void HandleEnabledStateChange(bool isEnabled)
        {
            _collectDisposable?.Dispose();
            _collectDisposable = new CompositeDisposable();
            if(isEnabled  == false) return;

            Observable
                .Interval(TimeSpan.FromSeconds(_resourceModel.SecondsToGain))
                .Subscribe(l =>
                    _resourceModel.ChangeResourceBy(1))
                .AddTo(_collectDisposable);

        }

        private void CollectResources()
        {
            var collectedResources = _resourceModel.ResourceCount.Value;
            _resourceModel.ChangeResourceBy(-collectedResources);
            _resourcesService.GetModel(_resourceModel.ResourceType).ChangeResourceBy(collectedResources);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _isEnabled?.Dispose();
            _collectDisposable?.Dispose();
        }
    }
}