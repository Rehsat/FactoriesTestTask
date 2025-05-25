using System.Collections.Generic;
using Game.Services.Audio;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.PopUps
{
    public class VolumeChangePopUp : PopUp
    {
        [SerializeField] private List<Slider> _volumeSliders;
        [Inject]
        public void Construct(IPopUpsSpawnService popUpsSpawner, IAudioService audioService)
        {
            // костыльненько, но в 3 часа ночи не особо горю желанием писать еще один презентер, как сделал это со списком ресурсов)
            popUpsSpawner.AddPopUp(PopUpType.VolumeChange,this);
            for (var index = 0; index < _volumeSliders.Count; index++)
            {
                var slider = _volumeSliders[index];
                var audioType = (GameAudio)(index+1);
                var volumeData = audioService.GetAudioVolume(audioType);
                slider.value = volumeData.Value;
                slider.onValueChanged.AddListener((newVolumeValue =>
                    audioService.SetVolume(audioType, newVolumeValue)));
            }
        }

        private void OnDestroy()
        {
            foreach (var slider in _volumeSliders)
            {
                slider.onValueChanged.RemoveAllListeners();
            }
        }
    }
}
