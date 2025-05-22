using Game.Infrastructure.AssetsManagement;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Factories
{
    public abstract class BaseSimpleFactory<TPrefabType> : IFactory<TPrefabType> where TPrefabType : MonoBehaviour
    {
        private TPrefabType _prefab;
        public BaseSimpleFactory(IPrefabsProvider prefabsProvider)
        {
            var prefabId = GetPrefabType();
            _prefab = prefabsProvider.GetPrefabsComponent<TPrefabType>(prefabId);
        }
        public TPrefabType Create()
        {
            var instantinatedObject = Object.Instantiate(
                _prefab); 
            if(instantinatedObject is IConstructable constructable)
                constructable.Construct();
            
            return instantinatedObject;
        }

        protected abstract Prefab GetPrefabType();
    }

    public interface IConstructable
    {
        public void Construct();
    }
}
