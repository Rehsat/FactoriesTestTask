using Game.Core.PlayerResourcess.ResourcFactories;
using Game.Infrastructure.AssetsManagement;

namespace Game.Factories
{
    public class ResourceUiViewFactory : BaseSimpleFactory<ResourceUiView>
    {
        public ResourceUiViewFactory(IPrefabsProvider prefabsProvider) : base(prefabsProvider)
        {
        }

        protected override Prefab GetPrefabType()
        {
            return Prefab.ResourceUiView;
        }
    }
}