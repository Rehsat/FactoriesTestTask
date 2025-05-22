using System;
using System.Collections;
using System.Collections.Generic;
using EasyFramework.ReactiveTriggers;
using RotaryHeart.Lib.SerializableDictionaryPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.PopUps
{
    public class PopUpsSpawner : MonoBehaviour, IPopUpsSpawnService
    {
        [SerializeField] private Image _backGround;
        
        Dictionary<PopUpType, PopUp> _popUps;
        private ReactiveTrigger _onHide;
        private CompositeDisposable _compositeDisposable;
        
        [Inject]
        public void Construct()
        {
            _compositeDisposable = new CompositeDisposable();
            _onHide = new ReactiveTrigger();
            _popUps = new Dictionary<PopUpType, PopUp>();
            _onHide.Subscribe((() =>
            {
                _backGround.gameObject.SetActive(false);
            }));
        }

        public void AddPopUp(PopUpType popUpType, PopUp popUp)
        {
            if (_popUps.ContainsKey(popUpType))
            {
                Debug.LogError($"Already contains {popUpType}");
                return;
            }
            _popUps.Add(popUpType, popUp);
            popUp.Construct(_onHide);
            popUp.transform.parent = this.transform;
            popUp.gameObject.SetActive(false);
            popUp.OnShowRequired
                .SubscribeWithSkip(() => SpawnPopUp(popUpType))
                .AddTo(_compositeDisposable);
        }

        public void SpawnPopUp(PopUpType popUpType, Action onComplete = null)
        {
            IDisposable subscribe = null;
            void OnComplete()
            {
                onComplete?.Invoke();
                subscribe?.Dispose();
            }
            subscribe = _onHide.SubscribeWithSkip(OnComplete);
        
            _backGround.gameObject.SetActive(true);
            var popUp = _popUps[popUpType];
            popUp.gameObject.SetActive(true);
            popUp.Show();
        }

        private void OnDestroy()
        {
            _compositeDisposable?.Dispose();
        }
    }
    public enum PopUpType
    {
        ResourcesCount
    }
}