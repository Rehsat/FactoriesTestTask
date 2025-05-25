using System;
using System.Collections.Generic;
using Game.Services.Save;
using UniRx;

namespace Game.Services.Audio
{
    public class AudioService : IAudioService, ISavable
    {
        private readonly Dictionary<GameAudio, ReactiveProperty<float>> _volume;
        public SaveDataId Id => SaveDataId.Audio;
        
        public AudioService()
        {
            _volume = new Dictionary<GameAudio, ReactiveProperty<float>>();
            foreach (GameAudio type in Enum.GetValues(typeof(GameAudio)))
                _volume.Add(type, new ReactiveProperty<float>(0.7f));
        }
        
        public IReadOnlyReactiveProperty<float> GetAudioVolume(GameAudio gameAudio)
        {
            return _volume[gameAudio];
        }

        public void SetVolume(GameAudio gameAudio, float value)
        {
            _volume[gameAudio].Value = value;
        }

        public void Save(SaveData saveData)
        {
            saveData.MusicVolume = _volume[GameAudio.Music].Value;
            saveData.SfxVolume = _volume[GameAudio.Sfx].Value;
        }

        public void Load(SaveData saveData)
        {
            _volume[GameAudio.Music].Value = saveData.MusicVolume;
            _volume[GameAudio.Sfx].Value = saveData.SfxVolume;
        }
    }
}