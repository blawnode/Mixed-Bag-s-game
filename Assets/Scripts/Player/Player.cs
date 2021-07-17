using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip batteryPickup;

    // battery
    [SerializeField] private UnityEngine.UI.Slider batterySlider;

    [SerializeField] private float lowBatteryThershold = .3f;

    [SerializeField] private float batteryUseInterval;
    private float batteryUseTimer;


    private bool isDead = false;

    // camera
    [SerializeField] private PlayerCamera playerCamera;

    // movement
    [SerializeField] private Vector2 speed = new Vector2(50f, 50f);

    private void Update()
    {
        if (isDead)
            return;

        batteryUseTimer += Time.deltaTime;
        if (batteryUseTimer >= batteryUseInterval)
        {
            batteryUseTimer = 0f;
            batterySlider.value = Mathf.Max(batterySlider.value - 0.01f, 0f);

            if (batterySlider.value < lowBatteryThershold)
                AudioManager.i.Play(AudioManager.AudioName.LowBatteryAlert);

            if (batterySlider.value == 0f && !isDead)
            {
                isDead = true;
                AudioManager.i.Play(AudioManager.AudioName.Death);

                GameObject loaderObject = GameObject.FindWithTag("SceneLoader");

                if (!loaderObject)
                    Debug.LogError("Failed to find SceneLoader");

                SceneLoader loader = loaderObject.GetComponent<SceneLoader>();
                loader.LoadScene("Game");
            }
        }
    }

    void FixedUpdate()
    {
        if (isDead)
            return;

        // Player movement
        Vector3 movement = new Vector3(speed.x * Input.GetAxis("Horizontal"), speed.y * Input.GetAxis("Vertical"), 0) * Time.fixedDeltaTime;
        transform.Translate(movement);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (isDead)
            return;

        Spawnable spawnable = c2d.GetComponent<Spawnable>();
        if (!spawnable)
            return;

        float value = spawnable.Value;

        if(c2d.CompareTag("Asteroid"))
        {
            if (value > 85)
                AudioManager.i.Play(AudioManager.AudioName.LargeCollision);
            else if (value > 35)
                AudioManager.i.Play(AudioManager.AudioName.MediumCollision);
            else
            {
                AudioManager.i.Play(AudioManager.AudioName.Ow);
                AudioManager.i.Play(AudioManager.AudioName.MinorCollision);
            }

            batterySlider.value = Mathf.Max(batterySlider.value - value / 100f, 0);
        }
        else if(c2d.CompareTag("Battery"))
        {
            AudioManager.i.Play(AudioManager.AudioName.BatteryPickup);
            batterySlider.value = Mathf.Max(batterySlider.value + value / 100f, 1);
        }
    }

}
