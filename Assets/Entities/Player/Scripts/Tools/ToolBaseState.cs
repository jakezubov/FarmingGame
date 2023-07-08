using UnityEngine;

public abstract class ToolBaseState : MonoBehaviour
{
    public abstract void UseTool(ToolStateManager toolSM, Vector3Int currentCell, Tool tool);
}
