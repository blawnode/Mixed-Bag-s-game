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

    [SerializeField] private GameObject flashlight;
    private bool isFlashlightOn = false;

    private bool isBreathingIn = true;  // false -> breathing out. Used by Breathe(), which's used by the animator
    private float lastBreatheTime = 0;
    private float breatheCooldownTime = 0.3f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
                loader.LoadDeathScene();
            }
        }

        if(flashlight != null)  // TODO: This here assumes the player uses a mouse.
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x);
            flashlight.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void FixedUpdate()
    {
        if (isDead)
            return;

        // Player movement
        Vector3 movement = new Vector3(speed.x * Input.GetAxis("Horizontal"), speed.y * Input.GetAxis("Vertical"), 0) * Time.fixedDeltaTime;
        transform.Translate(movement);

        // Animations
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        string chosenStateName = "";
        if (movement.x > 0)
        {
            if (isFlashlightOn)
                chosenStateName = "Flash E";
            else chosenStateName = "Idle E";
        }
        else if(movement.x < 0)
        {
            if (isFlashlightOn)
                chosenStateName = "Flash W";
            else chosenStateName = "Idle W";
        }
        else
        {
            if (isFlashlightOn)
                chosenStateName = "Flash S";
            else chosenStateName = "Idle S";
        }
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(chosenStateName))
        {
            animator.Play(chosenStateName, -1, normalizedTime);
        }
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

    // Used by the player's animator.
    public void Breathe()
    {
        if (Time.time - lastBreatheTime >= breatheCooldownTime)  // This condition prevents the breathe from happening too frequently
        {
            if (isBreathingIn)
                AudioManager.i.Play(AudioManager.AudioName.BreatheIn);
            else
                AudioManager.i.Play(AudioManager.AudioName.BreatheOut);
            isBreathingIn = !isBreathingIn;
            lastBreatheTime = Time.time;
        }
    }
}
