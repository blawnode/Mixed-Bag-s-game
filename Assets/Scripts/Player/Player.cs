using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    // oxygen
    [SerializeField] private Image o2Image;
    private float o2 = 150f;

    [SerializeField] private float lowO2Theshold = 30f;

    [SerializeField] private float o2UseInterval;
    private float o2useTimer;

    // health
    [SerializeField] private Image hpImage;
    private float hp = 100f;

    private bool isDead = false;

    // camera
    [SerializeField] private PlayerCamera playerCamera;

    // movement
    [SerializeField] private Vector2 speed = new Vector2(50f, 50f);

    // flashlight
    [SerializeField] private GameObject flashlight;
    [SerializeField] private Light2D light2d;

    // breath
    private bool isBreathingIn = true;  // false -> breathing out. Used by Breathe(), which's used by the animator
    private float lastBreatheTime = 0;
    private float breatheCooldownTime = 0.3f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (flashlight == null) Debug.LogError("Flashlight expected");
        if (light2d == null) Debug.LogError("Light2D expected");
        if (animator == null) Debug.LogError("Animator expected");

    }

    private void Update()
    {
        if (isDead)
            return;

        o2useTimer += Time.deltaTime;
        if (o2useTimer >= o2UseInterval)
        {
            o2UseInterval = 0f;
            o2 -= 0.01f;
            o2Image.fillAmount = o2 / 150f;

            if (o2 < lowO2Theshold)
                AudioManager.i.Play(AudioManager.AudioName.LowO2Alert);

            if ((o2 <= 0f || hp <= 0f) && !isDead)
            {
                isDead = true;
                AudioManager.i.Play(AudioManager.AudioName.Death);

                GameObject loaderObject = GameObject.FindWithTag("SceneLoader");

                if (!loaderObject)
                    Debug.LogError("Failed to find SceneLoader");

                SceneLoader loader = loaderObject.GetComponent<SceneLoader>();

                if (hp <= 0f)
                    loader.LoadScene("DeathByHp");
                if (o2 <= 0f)
                    loader.LoadScene("DeathByO2");
            }
        }

        float angle = Mathf.Rad2Deg * Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x);
        flashlight.transform.rotation = Quaternion.Euler(0, 0, angle);

        Debug.Log(flashlight.transform.rotation);

        if (Input.GetMouseButtonDown(0))
            ToggleFlashlight();
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
            if (light2d.enabled)
                chosenStateName = "Flash E";
            else chosenStateName = "Idle E";
        }
        else if (movement.x < 0)
        {
            if (light2d.enabled)
                chosenStateName = "Flash W";
            else chosenStateName = "Idle W";
        }
        else
        {
            if (light2d.enabled)
                chosenStateName = "Flash S";
            else chosenStateName = "Idle S";
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(chosenStateName))
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

            hp -= value;
            if (hp < 0)
                hp = 0;
            hpImage.fillAmount = Mathf.Max(hp / 100f, 0);
        }
        else if(c2d.CompareTag("Oxygen"))
        {
            AudioManager.i.Play(AudioManager.AudioName.O2Pickup);
            o2 += value;
            o2Image.fillAmount = o2 / 150f;
        }
    }

    // Used by the player's animator.
    public void Breathe()
    {
        // can we not????
        // if (Time.time - lastBreatheTime >= breatheCooldownTime)  // This condition prevents the breathe from happening too frequently
        // {
        //     if (isBreathingIn)
        //         AudioManager.i.Play(AudioManager.AudioName.BreatheIn);
        //     else
        //         AudioManager.i.Play(AudioManager.AudioName.BreatheOut);
        //     isBreathingIn = !isBreathingIn;
        //     lastBreatheTime = Time.time;
        // }
    }

    private void ToggleFlashlight()
    {
        light2d.enabled = !light2d.enabled;
        // flashlight.GetComponentInChildren<Light2D>().enabled = isFlashlightOn;
    }
}
