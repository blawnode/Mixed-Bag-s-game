using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public int score = 15;

    private float timer = 0f;
    public float delayAmount;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delayAmount)
        {
            timer = 0f;
            score+=100;
        }

        scoreText.text = "Score: "+(score).ToString();
    }
}
