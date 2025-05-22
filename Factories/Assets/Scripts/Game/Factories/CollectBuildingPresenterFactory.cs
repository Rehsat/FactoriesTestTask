using Game.Core.PlayerResourcess.ResourcFactories;
using Game.Services.PlayerResources;
using Zenject;

namespace Game.Factories
{
    public class CollectBuildingPresenterFactory : IFactory<PlayerResource, ResourceCollectBuildingPresenter>
    {
        private readonly IPlayerResourcesService _resourcesService;
        private readonly IFactory<PlayerResource, IResourceCollectBuilding> _viewFactory;

        public CollectBuildingPresenterFactory(
            IPlayerResourcesService resourcesService
            ,IFactory<PlayerResource, IResourceCollectBuilding> viewFactory)
        {
            _resourcesService = resourcesService;
            _viewFactory = viewFactory;
        }
        public ResourceCollectBuildingPresenter Create(PlayerResource resource)
        {
            var model = _resourcesService.GetModel(resource);
            var view = _viewFactory.Create(resource);
            return new ResourceCollectBuildingPresenter(model, view);
        }
    }
}