using UnityEngine;
using UnityEngine.InputSystem;

public class CustomCursor : MonoBehaviour
{
    public Transform _mCursorVisual;
    public Vector3 _mDisplacement;

    void Start()
    {
        // this sets the base cursor as invisible
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        position.z = Camera.main.nearClipPlane;
        _mCursorVisual.position = position + _mDisplacement;
    }
}
