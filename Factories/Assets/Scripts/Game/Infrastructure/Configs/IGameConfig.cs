using Game.Infrastructure.AssetsManagement;

namespace Game.Infrastructure.Configs
{
    public interface IGameConfig
    {
        public IPrefabsProvider PrefabsProvider { get; }
    }
}