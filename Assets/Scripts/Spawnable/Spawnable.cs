using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Spawnable : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;

    [SerializeField] private float minRotationSpeed, maxRotationSpeed;
    [SerializeField] private float minMovementSpeed, maxMovementSpeed;

    private float rotationSpeed, movementSpeed;
    private Vector2 movementDirection;

    [SerializeField] private float value;
    public float Value { get { return value; } }

    public void ShootTowards(Vector3 position)
    {
        movementDirection = (position - transform.position).normalized;

        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, 0f, 1f), rotationSpeed * Time.fixedDeltaTime);
        transform.Translate(movementDirection * movementSpeed * Time.fixedDeltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
            Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D c2d)
    {
        if (c2d.CompareTag("playArea"))
            Destroy(gameObject);
    }   
}