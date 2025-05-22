using System;
using System.Collections.Generic;
using Game.Core.PlayerResourcess.ResourcFactories;
using Game.Services.PlayerResources;
using Game.UI.PopUps;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Core.PlayerResourcess
{
    public class ResourcesPopUpPresenter : IDisposable
    {
        private readonly ListOfObjectsPopUp _listOfObjectsPopUp;
        private readonly IPlayerResourcesService _resourcesService;
        private readonly IFactory<ResourceUiView> _resourcesViewFactory;
        private readonly CompositeDisposable _compositeDisposable;
        private readonly List<Transform> _listOfViews;
        
        public ResourcesPopUpPresenter(
            ListOfObjectsPopUp listOfObjectsPopUp,
            IPlayerResourcesService resourcesService,
            IFactory<ResourceUiView> resourcesViewFactory)
        {
            _listOfObjectsPopUp = listOfObjectsPopUp;
            _resourcesService = resourcesService;
            _resourcesViewFactory = resourcesViewFactory;
            _listOfViews = new List<Transform>();
            _compositeDisposable = new CompositeDisposable();
            
            Initialize();
        }
        
        private void Initialize()
        {
            var allResources = _resourcesService.GetAllModels();
            allResources.ForEach(InitializeResource);
            _listOfObjectsPopUp.Construct(_listOfViews);
        }

        private void InitializeResource(PlayerResourceModel resourceModel)
        {
            var view = _resourcesViewFactory.Create();
            view.SetSprite(resourceModel.Sprite);
            view.SetTitle(resourceModel.ResourceType.ToString());
            resourceModel.ResourceCount
                .Subscribe(view.SetResourceCount)
                .AddTo(_compositeDisposable);
            
            _listOfViews.Add(view.transform);
        }

        public void ShowPopUp()
        {
            _listOfObjectsPopUp.RequestShow();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}
