using Game.Infrastructure.AssetsManagement;
using Game.UI.PopUps;
using UnityEngine;
using Zenject;

namespace Game.Factories
{
    public class ListOfObjectsPopUpFactory : IFactory<ListOfObjectsPopUp>
    {
        private readonly IPrefabsProvider _prefabProvider;
        private readonly IPopUpsSpawnService _popUpsSpawnService;

        public ListOfObjectsPopUpFactory(IPrefabsProvider prefabProvider, IPopUpsSpawnService popUpsSpawnService)
        {
            _prefabProvider = prefabProvider;
            _popUpsSpawnService = popUpsSpawnService;
        }
        public ListOfObjectsPopUp Create()
        {
            var prefab = _prefabProvider.GetPrefabsComponent<ListOfObjectsPopUp>(Prefab.ListOfObjectsPopUp);
            var spawnedPrefab = Object.Instantiate(prefab);
            _popUpsSpawnService.AddPopUp(PopUpType.ResourcesCount, spawnedPrefab);
            return spawnedPrefab;
        }
    }
}