using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] Text scoreText;

    private int totalScore;

    private void Awake()
    {
        totalScore = 0;
        UpdateScoreValue(0);
    }

    public void UpdateScoreValue(int score)
    {
        totalScore += score;
        scoreText.text = "Score: " + totalScore;
    }

    public void ResetScoreValue()
    {
        scoreText.text = "Score: 0";
    }
}
