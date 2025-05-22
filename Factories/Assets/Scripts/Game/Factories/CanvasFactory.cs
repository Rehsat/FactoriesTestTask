using UnityEngine;
using Zenject;

namespace Game.Factories
{
    public class CanvasFactory : IFactory<Canvas>
    {
        public Canvas Create()
        {
            return null;
        }
    }
}