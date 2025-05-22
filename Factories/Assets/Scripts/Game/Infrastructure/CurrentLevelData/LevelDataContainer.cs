using UnityEngine;
using Zenject;

namespace Game.Infrastructure.CurrentLevelData
{
    public class LevelDataContainer : MonoBehaviour
    {
        [SerializeField] private LevelData _levelData;

        [Inject]
        public void Construct(ICurrentLevelDataProvider currentLevelDataProvider)
        {
            currentLevelDataProvider.SetCurrentLevelData(_levelData);
        }
    }
}