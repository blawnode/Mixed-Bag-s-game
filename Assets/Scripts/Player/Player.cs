using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip batteryPickup;

    // battery
    [SerializeField] private UnityEngine.UI.Slider batterySlider;

    [SerializeField] private float batteryUseInterval;
    private float batteryUseTimer;

    private bool dead = false;

    // camera
    [SerializeField] private PlayerCamera playerCamera;

    // movement
    [SerializeField] private Vector2 speed = new Vector2(50f, 50f);

    private void Update()
    {
        if (dead)
            return;

        batteryUseTimer += Time.deltaTime;
        if (batteryUseTimer >= batteryUseInterval)
        {
            batteryUseTimer = 0f;
            batterySlider.value = Mathf.Max(batterySlider.value - 0.01f, 0f);

            if (batterySlider.value == 0f && !dead)
            {
                dead = true;
                AudioManager.i.Play(AudioManager.AudioName.Death);
            }
        }
    }

    void FixedUpdate()
    {
        if (dead)
            return;

        // Player movement
        Vector3 movement = new Vector3(speed.x * Input.GetAxis("Horizontal"), speed.y * Input.GetAxis("Vertical"), 0) * Time.fixedDeltaTime;
        transform.Translate(movement);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (dead)
            return;

        Spawnable spawnable = c2d.GetComponent<Spawnable>();
        if (!spawnable)
            return;

        float value = spawnable.Value;

        if(c2d.CompareTag("Asteroid"))
        {
            AudioManager.i.Play(AudioManager.AudioName.Ow);
            batterySlider.value = Mathf.Max(batterySlider.value - value / 100f, 0);
        }
        else if(c2d.CompareTag("Battery"))
        {
            AudioManager.i.Play(AudioManager.AudioName.BatteryPickup);
            batterySlider.value = Mathf.Max(batterySlider.value + value / 100f, 1);
        }
    }

}
