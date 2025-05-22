using Game.Infrastructure.AssetsManagement;
using Game.Services.PlayerResources;

namespace Extensions
{
    public static class PlayerResourceExtension
    {
        public static Prefab ToPrefab(this PlayerResource playerResource)
        {
            switch (playerResource)
            {
                case PlayerResource.Bread:
                    return Prefab.BreadFactory;
                
                case PlayerResource.Cheese:
                    return Prefab.CheeseFactory;
                
                case PlayerResource.Pickles:
                    return Prefab.PicklesFactory;
                
                case PlayerResource.Sausages:
                    return Prefab.SausagesFactory;
                
                case PlayerResource.Tomatoes:
                    return Prefab.TomatoesFactory;
                
                default:
                    return Prefab.None;
            }
        }
    }
}
