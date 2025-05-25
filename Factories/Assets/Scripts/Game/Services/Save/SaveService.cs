using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Services.Save
{
    public class SaveService : ISaveService
    {
        private readonly IDataSerializer _dataSerializer;
        private Dictionary<SaveDataId, ISavable> _saveables;

        private const string SAVE_KEY = "SAVE_KEY";
        public SaveService(List<ISavable> savables, 
            IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
            _saveables = new Dictionary<SaveDataId, ISavable>();
            savables.ForEach(savable => _saveables.Add(savable.Id, savable));
            Load();
            //При необходимости можно будет добавить более сложную логику сейвов
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe((l => Save()));
        }
        
        public void Save()
        {
            var saveData = new SaveData();
            foreach (var keyValuePair in _saveables)
            {
                keyValuePair.Value.Save(saveData);
            }
            var saveDataSerialized = _dataSerializer.Serialize(saveData);
            PlayerPrefs.SetString(SAVE_KEY, saveDataSerialized);
        }

        public void Load()
        {
            var savedDataSerialized = PlayerPrefs.GetString(SAVE_KEY);
            var savedData = _dataSerializer.Deserialize<SaveData>(savedDataSerialized);
            if(savedData == null) return;
            
            foreach (var saveablesValue in _saveables.Values)
            {
                saveablesValue.Load(savedData);
            }
        }
        
    }
}