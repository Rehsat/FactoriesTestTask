using System;
using System.Collections.Generic;
using Game.Infrastructure.Configs;

namespace Game.Services.PlayerResources
{
    public class PlayerResourcesService : IPlayerResourcesService
    {
        private readonly ISpriteByResourceTypeContainer _spriteByResourceTypeContainer;
        private readonly Dictionary<PlayerResource, PlayerResourceModel> _playerResources;

        public PlayerResourcesService(ISpriteByResourceTypeContainer spriteByResourceTypeContainer)
        {
            _spriteByResourceTypeContainer = spriteByResourceTypeContainer;
            _playerResources = new Dictionary<PlayerResource, PlayerResourceModel>();
            foreach (PlayerResource resourceType in Enum.GetValues(typeof(PlayerResource)))
                _playerResources.Add(resourceType, Create(resourceType));
        }

        private PlayerResourceModel Create(PlayerResource resource)
        {
            var sprite = _spriteByResourceTypeContainer.GetSprite(resource);
            var model = new PlayerResourceModel(sprite);
            return model;
        }

        public PlayerResourceModel GetModel(PlayerResource playerResource)
        {
            return _playerResources[playerResource];
        }
    }
}
