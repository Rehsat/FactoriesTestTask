using EasyFramework.ReactiveTriggers;

namespace Game.Core.Interactables
{
    public interface IInteractable
    {
        public IReadOnlyReactiveTrigger OnInteract { get; }
        public void Interact();
    }
}