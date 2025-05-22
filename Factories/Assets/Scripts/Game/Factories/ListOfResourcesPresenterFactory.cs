using Game.Core.PlayerResourcess;
using Game.Core.PlayerResourcess.ResourcFactories;
using Game.Services.PlayerResources;
using Game.UI.PopUps;
using Zenject;

namespace Game.Factories
{
    public class ListOfResourcesPresenterFactory : IFactory<ResourcesPopUpPresenter>
    {
        private readonly IFactory<ListOfObjectsPopUp> _popUpFactory;
        private readonly IPlayerResourcesService _playerResourcesService;
        private readonly IFactory<ResourceUiView> _factoryOfResourcesView;

        public ListOfResourcesPresenterFactory(IFactory<ListOfObjectsPopUp> popUpFactory,
            IPlayerResourcesService playerResourcesService,
            IFactory<ResourceUiView> factoryOfResourcesView)
        {
            _popUpFactory = popUpFactory;
            _playerResourcesService = playerResourcesService;
            _factoryOfResourcesView = factoryOfResourcesView;
        }
        public ResourcesPopUpPresenter Create()
        {
            var popUp = _popUpFactory.Create();
            var presenter = new ResourcesPopUpPresenter(popUp, _playerResourcesService, _factoryOfResourcesView);
            return presenter;
        }
    }
}