using UnityEngine;

public abstract class ControlsBaseState : MonoBehaviour
{
    public abstract void UseControls(PlayerController controller, SpellbookAnimations spellbook, Map map);
}
