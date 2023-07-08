using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private static PlayerActionControls _playerActionControls;
    private static bool _isInteracting = false, _isMapOpen = false;

    private void Awake()
    {
        _playerActionControls = new PlayerActionControls();
    }

    private void OnEnable() { _playerActionControls.Enable(); }
    private void OnDisable() { _playerActionControls.Disable(); }

    public static PlayerActionControls GetControls()
    {
        return _playerActionControls;
    }

    public static (float, float) GetPlayerMovements()
    {
        float movementHoriztonal = _playerActionControls.General.MoveHorizontal.ReadValue<float>();
        float movementVertical = _playerActionControls.General.MoveVertical.ReadValue<float>();

        return (movementHoriztonal, movementVertical);
    }

    public static bool IsInteracting()
    {
        return _isInteracting;
    }

    public static void SetInteracting(bool value)
    {
        _isInteracting = value;
    }

    public static bool IsMapOpen()
    {
        return _isMapOpen;
    }

    public static void SetMapOpen(bool value)
    {
        _isMapOpen = value;
    }
}
