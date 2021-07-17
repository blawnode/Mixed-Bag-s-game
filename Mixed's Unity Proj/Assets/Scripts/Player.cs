using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerCamera playerCamera;

    [SerializeField] private Vector2 speed = new Vector2(50f, 50f);
    public static int impact = 0;
    public static int battery = 0;

    void FixedUpdate()
    {
        // player camera
        float rawX = Input.GetAxisRaw("Horizontal");
        float rawY = Input.GetAxisRaw("Vertical");
        playerCamera.UpdateOffset(new Vector2(rawX == 0f ? 0f : Mathf.Sign(rawX), rawY == 0f ? 0f : Mathf.Sign(rawY)));

        // player movement
        Vector3 movement = new Vector3(speed.x * Input.GetAxis("Horizontal"), speed.y * Input.GetAxis("Vertical"), 0) * Time.fixedDeltaTime;
        transform.Translate(movement);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("smallAsteroid"))
        {
            impact = 10;
        }
        else if (c2d.CompareTag("medAsteroid"))
        {
            impact = 20;
        }
        else if (c2d.CompareTag("largeAsteroid"))
        {
            impact = 30;
        }
        else if (c2d.CompareTag("Battery"))
        {
            battery = 100;
        }
    }
}
