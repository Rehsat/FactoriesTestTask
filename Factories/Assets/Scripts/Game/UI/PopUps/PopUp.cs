using System.Collections.Generic;
using DG.Tweening;
using EasyFramework.ReactiveTriggers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.PopUps
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private List<Button> _allCloseButtons;
        
        private ReactiveTrigger _onHide;
        private ReactiveTrigger _onShowRequired;
        private bool _isHiding;

        public ReactiveTrigger OnShowRequired => _onShowRequired;

        public void Construct(ReactiveTrigger onHide)
        {
            _onHide = onHide;
            _onShowRequired = new ReactiveTrigger();
            _allCloseButtons.ForEach(closeButton => closeButton.onClick.AddListener(Hide));
        }

        public void Hide()
        {
            if(_isHiding) return;
            _isHiding = true;
            transform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete((() =>
            {
                _isHiding = false;
                _onHide.Notify();
            }));
        }

        public void RequestShow()
        {
            _onShowRequired.Notify();
        }

        public void Show()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
        }
    }
}
