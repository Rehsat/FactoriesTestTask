using Game.Infrastructure.AssetsManagement;
using Game.Infrastructure.CurrentLevelData;

namespace Game.Factories
{
    public class HudFactory : BaseSimpleFactory<HUDView>
    {
        public HudFactory(IPrefabsProvider prefabsProvider) : base(prefabsProvider)
        {
        }

        protected override Prefab GetPrefabType()
        {
            return Prefab.HUD;
        }
    }
}