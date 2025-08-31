using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    private int currentScore;
    private int maxScore;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = "Score : " + currentScore + " / " + maxScore;
    }

    public void SetMaxScore(int value)
    {
        maxScore = value;
        UpdateUI();
    }

    public void AddScore(int value)
    {
        currentScore += value;
        UpdateUI();
    }
}
