﻿using EasyFramework.ReactiveTriggers;
using Game.Core.UtilityInterfaces;

namespace Game.Core.PlayerResourcess.ResourcFactories
{
    public interface IResourceCollectBuilding : IGameObjectModelRequier, ISpriteRequier, ITitleRequier
    {

        public IReadOnlyReactiveTrigger OnCollectResource { get; }
        public void SetResourceViewCount(float resourceCount);
    }
}