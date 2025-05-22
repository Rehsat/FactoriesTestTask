using EasyFramework.ReactiveEvents;
using UniRx;
using UnityEngine;

namespace Game.Core.Player.Movement
{
    public interface IWorldPointContainer
    {
        public IReadOnlyReactiveProperty<Vector3> CurrentPoint { get; }
    }
}