using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    [SerializeField] private float scoreInterval;
    private float scoreTimer = 0f;

    void Update()
    {
        scoreTimer += Time.deltaTime;

        if (scoreTimer >= scoreInterval)
        {
            scoreTimer = 0f;

            score += 100;
            scoreText.text = "Score: " + score.ToString();
        }

    }
}
