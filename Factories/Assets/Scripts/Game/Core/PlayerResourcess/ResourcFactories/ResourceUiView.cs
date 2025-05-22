using Game.Core.UtilityInterfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.PlayerResourcess.ResourcFactories
{
    public class ResourceUiView : MonoBehaviour, ISpriteRequier
    {
        [SerializeField] private Image _resourceIcon;
        [SerializeField] private TMP_Text _resourceCountText;

        public void SetSprite(Sprite sprite)
        {
            _resourceIcon.sprite = sprite;
        }

        public void SetResourceCount(float resourceCount)
        {
            _resourceCountText.text = resourceCount.ToString();
        }
    }
}