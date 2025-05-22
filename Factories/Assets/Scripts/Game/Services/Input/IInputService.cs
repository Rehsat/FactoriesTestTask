using EasyFramework.ReactiveEvents;
using EasyFramework.ReactiveTriggers;
using UniRx;
using UnityEngine;

namespace Game.Services.Input
{
    public interface IInputService
    {
        public Vector2 PointerPosition { get; }
        public IReadOnlyReactiveProperty<Vector2> CurrentMovement { get; }
        public IReadOnlyReactiveEvent<ActionState> OnPressStateChanged { get; }
        public IReadOnlyReactiveTrigger OnInputUpdate { get; }
        public void SetCurrentMovement(Vector2 movement);
    }
}