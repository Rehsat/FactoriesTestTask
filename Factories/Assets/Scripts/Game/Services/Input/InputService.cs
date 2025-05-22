using System;
using EasyFramework.ReactiveEvents;
using EasyFramework.ReactiveTriggers;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Services.Input
{
    public class InputService : IInputService, IDisposable
    {
        private readonly InputActions _inputActions;
        private readonly ReactiveTrigger _onInputUpdate;
        private readonly CompositeDisposable _compositeDisposable;
        private readonly ReactiveEvent<ActionState> _onDragActionStateChanged;
        private readonly ReactiveProperty<Vector2> _currentMovement;
        public IReadOnlyReactiveProperty<Vector2> CurrentMovement => _currentMovement;
        public IReadOnlyReactiveEvent<ActionState> OnPressStateChanged => _onDragActionStateChanged;
        public IReadOnlyReactiveTrigger OnInputUpdate => _onInputUpdate;

        public Vector2 PointerPosition => Mouse.current.position.ReadValue();

        public InputService()
        {
            _currentMovement = new ReactiveProperty<Vector2>();
            _onDragActionStateChanged = new ReactiveEvent<ActionState>();
            _onInputUpdate = new ReactiveTrigger();
            _compositeDisposable = new CompositeDisposable();
            
            _inputActions = new InputActions();
            _inputActions.Enable();
            
            _inputActions.Touch.Touch.performed += ctx => _onDragActionStateChanged.Notify(ActionState.Started);
            _inputActions.Touch.Touch.canceled += ctx => _onDragActionStateChanged.Notify(ActionState.Completed);

            Observable
                .EveryUpdate()
                .Subscribe(f =>
                    _onInputUpdate.Notify())
                .AddTo(_compositeDisposable);
        }

        public void SetCurrentMovement(Vector2 movement)
        {
            _currentMovement.Value = movement;
        }
        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _inputActions.Disable();
        }
    }
}
