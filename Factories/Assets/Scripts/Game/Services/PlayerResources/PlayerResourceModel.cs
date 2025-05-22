using UniRx;
using UnityEngine;

namespace Game.Services.PlayerResources
{
    public class PlayerResourceModel
    {
        private ReactiveProperty<float> _resourceCount;
        private PlayerResource _resourceType;

        public Sprite Sprite { get; }
        public IReadOnlyReactiveProperty<float> ResourceCount => _resourceCount;

        public PlayerResourceModel(Sprite sprite)
        {
            _resourceCount = new ReactiveProperty<float>();
            Sprite = sprite;
        }

        public void ChangeResourceBy(float changeValue)
        {
            _resourceCount.Value += changeValue;
        }
    }
}