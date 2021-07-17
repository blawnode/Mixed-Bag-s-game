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
        Vector2 normalized = (target.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
        transform.position = Vector3.SmoothDamp(transform.position, target.position - (Vector3)(normalized * maxOffsetLength), ref velocity, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

}