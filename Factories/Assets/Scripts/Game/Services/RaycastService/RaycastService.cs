using Game.Services.Cameras;
using UnityEngine;

namespace Game.Services.RaycastService
{
    public class RaycastService : IRaycastService
    {
        private readonly ICameraService _cameraService;

        public RaycastService(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }
        
        public RaycastHit DoRaycast(Vector2 screenPosition)
        {
            Ray ray = _cameraService.MainCamera.ScreenPointToRay(screenPosition);
            Physics.Raycast(ray, out RaycastHit hit);
            return hit;
        }

        public bool TryGetComponentInRaycastHit<TComponent>(Vector2 screenPosition, out TComponent component)
        {
            var hit = DoRaycast(screenPosition);
        
            if (hit.collider != null &&
                hit.collider.TryGetComponent(out component))
                return true;

            component = default;
            return false;
        }
    }
}