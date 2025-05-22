using System;
using UniRx;

namespace Game.Core.Player.Movement
{
    public interface IMovementObserver
    {
        public IReadOnlyReactiveProperty<bool> IsMoving { get; }
    }
}