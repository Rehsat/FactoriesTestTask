using System.Collections.Generic;

namespace Game.Services.PlayerResources
{
    public interface IPlayerResourcesService
    {
        public PlayerResourceModel GetModel(PlayerResource playerResource);
        public List<PlayerResourceModel> GetAllModels();
    }
}