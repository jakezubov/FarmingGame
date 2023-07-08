using UnityEngine;

public class BaseAnimations : MonoBehaviour
{
    public ToolStateManager _tools;
    public Animator _baseAnimator;

    public void BaseAnimationMid()
    {
        _tools.UseItemInSlot();
    }

    public void BaseAnimationFinish()
    {
        PlayerActions.SetInteracting(false);
        _baseAnimator.SetBool("UseTool", false);
    }

    public void FishingInAnimationFinish()
    {
        _tools.UseItemInSlot();
        _baseAnimator.SetBool("PullRod", true);
    }
}
