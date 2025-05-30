using UnityEngine;

namespace Game.Services.RaycastService
{
    public interface IRaycastService
    {
        public RaycastHit DoRaycast(Vector2 screenPosition);
        public bool TryGetComponentInRaycastHit<TComponent>(Vector2 screenPosition, out TComponent component);
    }
}
