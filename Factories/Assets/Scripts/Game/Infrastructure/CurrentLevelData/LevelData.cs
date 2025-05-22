using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Infrastructure.CurrentLevelData
{
    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public Transform PlayerSpawnPosition { get; private set; }
        [field: SerializeField] public HUDView HudView { get; private set; } // Костылек получается, для ускорения
        [field: SerializeField] public List<Transform> FactoriesSpawnPoints { get; private set; }
    }
}