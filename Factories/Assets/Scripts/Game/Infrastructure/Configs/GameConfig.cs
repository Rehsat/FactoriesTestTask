using Game.Infrastructure.AssetsManagement;
using UnityEngine;

namespace Game.Infrastructure.Configs
{
    /*
    Думаю, что покрытием интерфейсами IGameConfig смог закрыть задачу 3 из требований:
    Нужно учесть что источником конфигурации игры могут стать разные источники данных 
    (в игре может быть 1 реализация - из ScriptableObject);
    */
    [CreateAssetMenu(menuName = "GameConfigs/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [SerializeField] private PrefabsProviderConfig _prefabsProviderConfig;
        [SerializeField] private SpriteByResourceTypeContainer _spriteByResourceTypeContainer;
        public IPrefabsProvider PrefabsProvider => _prefabsProviderConfig;
        public ISpriteByResourceTypeContainer SpriteByResourceTypeContainer => _spriteByResourceTypeContainer;
    }
}
