using UniRx;
using UnityEngine;

namespace Game.Services.Audio
{
    public interface IAudioService
    {
        public IReadOnlyReactiveProperty<float> GetAudioVolume(GameAudio gameAudio);
        public void SetVolume(GameAudio gameAudio, float value);
    }
}