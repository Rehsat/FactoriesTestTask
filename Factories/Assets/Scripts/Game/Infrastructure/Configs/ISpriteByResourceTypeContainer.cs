using Game.Services.PlayerResources;
using UnityEngine;

namespace Game.Infrastructure.Configs
{
    public interface ISpriteByResourceTypeContainer
    {
        public Sprite GetSprite(PlayerResource resource);
    }
}