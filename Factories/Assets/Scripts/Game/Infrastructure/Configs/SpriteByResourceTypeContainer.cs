using System;
using Game.Services.PlayerResources;
using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;

namespace Game.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "GameConfigs/SpriteByResourceTypeContainer", fileName = "SpriteByResourceTypeContainer")]
    public class SpriteByResourceTypeContainer : ScriptableObject, ISpriteByResourceTypeContainer
    {
        [SerializeField] private SerializableDictionary<PlayerResource, Sprite> _sprites;
        public Sprite GetSprite(PlayerResource resource)
        {
            if (_sprites.ContainsKey(resource))
                return _sprites[resource];
            
            throw new Exception($"There is no sprite for resource {resource}");
        }
    }
}