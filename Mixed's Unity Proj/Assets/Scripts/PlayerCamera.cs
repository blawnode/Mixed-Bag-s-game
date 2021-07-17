using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxOffsetLength;
    [SerializeField] private float smoothTime;

    private Vector3 offset = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);
    }

    public void UpdateOffset(Vector2 playerVelocityDirection /* this can only be +1 or -1 or 0 */)
    {
        offset = new Vector3(maxOffsetLength * playerVelocityDirection.x, maxOffsetLength * playerVelocityDirection.y, 0f);
    }
}