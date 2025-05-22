using Game.Core.Interactables;
using Game.Core.Player.Movement;
using Game.Services.Input;
using Game.Services.RaycastService;
using UniRx;
using UnityEngine;

namespace Game.Core.Player
{
    public class RaycastInteracter 
    {
        private readonly IInputService _inputService;
        private readonly IRaycastService _raycastService;
        private readonly IWorldPointContainer _playerPointToMoveContainer;

        private CompositeDisposable _compositeDisposable;

        public RaycastInteracter(
            IInputService inputService
            ,IRaycastService raycastService
            ,IWorldPointContainer playerPointToMoveContainer)
        {
            _inputService = inputService;
            _raycastService = raycastService;
            _playerPointToMoveContainer = playerPointToMoveContainer;
            _compositeDisposable = new CompositeDisposable();
            
            _inputService.OnPressStateChanged
                .SubscribeWithSkip(state =>
                {
                    if (state == ActionState.Started)
                        Raycast();
                })
                .AddTo(_compositeDisposable);
        }

        private void Raycast()
        {
            var raycastHit = _raycastService.DoRaycast(_inputService.PointerPosition);
            if(raycastHit.collider == null) return;

            Debug.LogError(raycastHit.collider.gameObject.name);
            var hitPosition = raycastHit.point;
            _playerPointToMoveContainer.SetNewPoint(hitPosition);
            
            if(raycastHit.collider.gameObject.TryGetComponent<IInteractable>(out var interactable))
                interactable.Interact();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}
