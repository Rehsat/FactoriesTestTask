using Game.Services.Input;
using UnityEngine;
using UniRx;
using Zenject;

namespace Game.Services.Cameras
{
    // В обновлениях можно будет добавить управление камерами (переключение и т.п)
    public class CameraService : MonoBehaviour, ICameraService
    {
        [SerializeField] private float _cameraSpeed;
        [SerializeField] private Camera _mainCamera;
        public Camera MainCamera => _mainCamera;

        [Inject]
        public void Construct(IInputService inputService)
        {
            inputService.OnInputUpdate.Subscribe(() =>
            {
                var moveDirection = inputService.CurrentMovement.Value;
                var direction = new Vector3(moveDirection.x, 0, moveDirection.y);
                _mainCamera.transform.position += direction * _cameraSpeed * Time.deltaTime;
            });
        }
    }
}