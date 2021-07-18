using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform subject;

    Vector2 startPosition;
    float startZ;


    Vector2 travel => (Vector2)camera.transform.position - startPosition;

    float distanceFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (camera.transform.position.z + (distanceFromSubject > 0 ? camera.farClipPlane : camera.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    private void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    private void Update()
    {
        Vector2 newPos = transform.position = startPosition + travel * 0.9f;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }

}
