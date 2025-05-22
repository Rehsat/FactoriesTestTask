using Game.Services.PlayerResources;
using UniRx;
using UnityEngine;

namespace Game.Core.PlayerResourcess.ResourcFactories
{
    public class ResourceCollectBuildingPresenter
    {
        private readonly PlayerResourceModel _resourceModel;
        private readonly IResourceCollectBuilding _resourceView;
        private readonly CompositeDisposable _compositeDisposable;
        
        public ResourceCollectBuildingPresenter(
            PlayerResourceModel resourceModel
            ,IResourceCollectBuilding resourceView)
        {
            _resourceModel = resourceModel;
            _resourceView = resourceView;
            _compositeDisposable = new CompositeDisposable();

            Initialize();
        }

        private void Initialize()
        {
            
        }

        public void SetCollectEnabled(bool isEnabled)
        {
        }
        public void SetPosition(Vector3 position)
        {
            if (_resourceView is MonoBehaviour resourceView)
                resourceView.transform.position = position;
        }
    }
}