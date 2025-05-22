using Game.Core.Interactables;
using Game.Core.Player.Animations;
using Game.Core.Player.Interaction;
using Game.Core.Player.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Core.Player
{
    public class PlayerRoot : MonoBehaviour, IInteractCallbackReciever
    {
        [SerializeField] private PlayerToPointMovement _playerToPointMovement;
        [SerializeField] private PlayerAnimator _playerAnimator;

        private PlayerInteractCallbackReciever _interactCallbackReciever;
        public void Construct(IWorldPointContainer pointToMove)
        {
            _playerToPointMovement.Construct(pointToMove);
            _playerAnimator.Construct(_playerToPointMovement);
            
            _interactCallbackReciever = new PlayerInteractCallbackReciever(
                _playerToPointMovement, 
                _playerAnimator);
        }

        public void SendCallback(InteractCallback callback)
        {
            _interactCallbackReciever.SendCallback(callback);
        }
    }

}
