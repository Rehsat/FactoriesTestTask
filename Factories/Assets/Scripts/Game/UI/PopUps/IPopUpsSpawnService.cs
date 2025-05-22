using System;

namespace Game.UI.PopUps
{
    public interface IPopUpsSpawnService
    {
        public void AddPopUp(PopUpType popUpType, PopUp popUp);
        public void SpawnPopUp(PopUpType popUpType, Action onComplete = null);
    }
}