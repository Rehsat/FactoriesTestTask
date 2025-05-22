using Game.Core.Player.Animations;
using Game.Core.Player.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Core.Player
{
    public class PlayerRoot : MonoBehaviour
    {
        [SerializeField] private PlayerToPointMovement _playerToPointMovement;
        [SerializeField] private PlayerAnimator _playerAnimator;

        public void Construct(IWorldPointContainer pointToMove)
        {
            _playerToPointMovement.Construct(pointToMove);
            _playerAnimator.Construct(_playerToPointMovement);
        }
    }

}
