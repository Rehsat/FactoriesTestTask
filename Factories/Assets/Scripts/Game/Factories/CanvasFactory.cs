using Game.Infrastructure.AssetsManagement;
using UnityEngine;
using Zenject;

namespace Game.Factories
{
    public class CanvasFactory : IFactory<Canvas>
    {
        private readonly IPrefabsProvider _prefabsProvider;
        public CanvasFactory(IPrefabsProvider prefabsProvider)
        {
            _prefabsProvider = prefabsProvider;
        }
        public Canvas Create()
        {
            var prefab = _prefabsProvider.GetPrefabsComponent<Canvas>(Prefab.Canvas);
            return Object.Instantiate(prefab);
        }
    }
}