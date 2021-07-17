using UnityEngine;

public class Player : MonoBehaviour
{
    // battery
    [SerializeField] private UnityEngine.UI.Slider batterySlider;

    [SerializeField] private float batteryUseInterval;
    private float batteryUseTimer;

    // camera
    [SerializeField] private PlayerCamera playerCamera;

    // movement
    [SerializeField] private Vector2 speed = new Vector2(50f, 50f);

    private void Update()
    {
        batteryUseTimer += Time.deltaTime;
        if (batteryUseTimer >= batteryUseInterval)
        {
            batteryUseTimer = 0f;
            batterySlider.value = Mathf.Max(batterySlider.value - 0.01f, 0);
        }

        if (batterySlider.value == 0)
            Debug.LogWarning("WE SHOULD BE DYING BRUH");
    }

    void FixedUpdate()
    {
        // Player movement
        Vector3 movement = new Vector3(speed.x * Input.GetAxis("Horizontal"), speed.y * Input.GetAxis("Vertical"), 0) * Time.fixedDeltaTime;
        transform.Translate(movement);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        Spawnable spawnable = c2d.GetComponent<Spawnable>();
        if (!spawnable)
            return;

        float value = spawnable.Value;

        if(c2d.CompareTag("Asteroid"))
            batterySlider.value = Mathf.Max(batterySlider.value - value / 100f, 0);
        else if(c2d.CompareTag("Battery"))
            batterySlider.value = Mathf.Max(batterySlider.value + value / 100f, 1);
    }

}
