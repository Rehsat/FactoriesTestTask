using DG.Tweening;
using UnityEngine;

public static class DOTweenCustomAnimations
{
    public static void DOJumpAnimation(this Transform transformToAnimate, float secondsToAnimate)
    {
        var startScale = transformToAnimate.localScale;
        transformToAnimate
            .DOScale(transformToAnimate.localScale * 1.2f, secondsToAnimate)
            .SetEase(Ease.OutCubic)
            .SetLoops(2, LoopType.Yoyo)
            .OnKill(() => transformToAnimate.localScale = startScale);
    }
}
