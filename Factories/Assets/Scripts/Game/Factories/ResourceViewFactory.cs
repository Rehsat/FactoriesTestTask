using Extensions;
using Game.Core.PlayerResourcess.ResourcFactories;
using Game.Infrastructure.AssetsManagement;
using Game.Services.PlayerResources;
using UnityEngine;
using Zenject;

namespace Game.Factories
{
    public class ResourceViewFactory : IFactory<PlayerResource, IResourceCollectBuilding>
    {
        private readonly IPrefabsProvider _prefabsProvider;

        public ResourceViewFactory(IPrefabsProvider prefabsProvider)
        {
            _prefabsProvider = prefabsProvider;
        }

        public IResourceCollectBuilding Create(PlayerResource resource)
        {
            var modelPrefabType = resource.ToPrefab();
            var modelPrefab = _prefabsProvider.GetPrefab(modelPrefabType);
            var model = Object.Instantiate(modelPrefab);
            
            var factoryPrefab = _prefabsProvider.GetPrefabsComponent<ResourceCollectBuilding>(Prefab.ResourceFactoryView);
            var factoryView = Object.Instantiate(factoryPrefab);
            factoryView.Construct();
            factoryView.SetModel(model);
            
            return factoryView;
        }
    }
}