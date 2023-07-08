using UnityEngine;

public class ToolAnimations : MonoBehaviour
{
    public ToolStateManager _tools;
    public Animator _toolAnimator;

    public void ToolAnimationMid()
    {
        _tools.UseItemInSlot();
    }

    public void ToolAnimationFinish()
    {
        _toolAnimator.SetBool("UseTool", false);
    }

    public void FishingRodInAnimationFinish()
    {
        _tools.UseItemInSlot();
        _toolAnimator.SetBool("PullRod", true);
    }
}
