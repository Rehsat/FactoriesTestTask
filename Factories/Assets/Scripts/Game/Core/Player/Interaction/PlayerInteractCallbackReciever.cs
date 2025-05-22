using System;
using System.Collections.Generic;
using Game.Core.Interactables;
using Game.Core.Player.Animations;
using Game.Core.Player.Movement;

namespace Game.Core.Player.Interaction
{
    public class PlayerInteractCallbackReciever : IInteractCallbackReciever
    {
        private readonly IStopableMovement _playerMovement;
        private readonly IPlayerAnimator _animator;
        private readonly Dictionary<InteractCallback, Action> _actionsByCallbackType;

        public PlayerInteractCallbackReciever(IStopableMovement playerMovement, IPlayerAnimator animator)
        {
            _playerMovement = playerMovement;
            _animator = animator;
            _actionsByCallbackType = new Dictionary<InteractCallback, Action>
            {
                {InteractCallback.ResourceCollected, HandleResourceCollected}
            };
        }
        public void SendCallback(InteractCallback callback)
        {
            if(_actionsByCallbackType.ContainsKey(callback))
                _actionsByCallbackType[callback].Invoke();
        }

        public void HandleResourceCollected()
        {
            _playerMovement.Stop();
            _animator.PlayAnimation(PlayerAnimation.HappyDance);
        }
    }
}
