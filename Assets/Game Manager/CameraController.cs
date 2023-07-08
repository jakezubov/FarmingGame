using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _target;
    public float _smoothing;

    void LateUpdate()
    {
        if (transform.position != _target.position)
        {
            Vector3 targetPos = new (_target.position.x, _target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, _smoothing);
        }
    }
}
