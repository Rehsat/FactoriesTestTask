using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Infrastructure.CurrentLevelData
{
    public class HUDView : MonoBehaviour
    {
        [field: SerializeField] public Button ResourcesPopUpButton { get; private set; }
        [field: SerializeField] public Button SettingsPopUpButton { get; private set; }
    }
}