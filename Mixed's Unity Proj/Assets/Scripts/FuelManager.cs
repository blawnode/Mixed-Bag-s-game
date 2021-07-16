using UnityEngine;
using TMPro;

public class FuelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fuelText;
    [SerializeField] UnityEngine.UI.Slider fuelSlider;

    const int FUEL_PACK_VALUE = 20;
    const int ASTEROID_CLASH_VALUE = 10;

    private float nextActionTime = 0.0f;
    public float period = 1f;

    private void Update()
    {
        if (Time.time > nextActionTime)  // https://answers.unity.com/questions/17131/execute-code-every-x-seconds-with-update.html
        {
            nextActionTime += period;
            fuelSlider.value = Mathf.Max(fuelSlider.value - 0.01f, 0);
            UpdateFuelText();
        }

        if (fuelSlider.value == 0)
        {
            print("*death*");
            // TODO: Send message to end game
        }
    }

    public void GetFuel()
    {
        fuelSlider.value = Mathf.Min(fuelSlider.value + FUEL_PACK_VALUE / 100f, 1);
        UpdateFuelText();
    }

    public void LoseFuel()
    {
        fuelSlider.value = Mathf.Max(fuelSlider.value - ASTEROID_CLASH_VALUE / 100f, 0);
        UpdateFuelText();
    }

    private void UpdateFuelText()
    {
        fuelText.text = ((int)(fuelSlider.value * 100)).ToString() + "%";
    }
}
