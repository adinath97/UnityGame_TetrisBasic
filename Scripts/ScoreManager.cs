using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI linesText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI highLevelText;
    [SerializeField] TextMeshProUGUI highLinesText;

    private int score, lines, level, bonusIncrease;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpScene();
    }

    private void SetUpScene() {
        scoreText.text = score.ToString();
        linesText.text = lines.ToString();
        levelText.text = level.ToString();

        highScoreText.text = PlayerPrefs.GetInt("HighScore",0).ToString();
        highLevelText.text = PlayerPrefs.GetInt("HighLevel",0).ToString();
        highLinesText.text = PlayerPrefs.GetInt("HighLines",0).ToString();
    }

    public void IncrementLines() { 
        lines++; 
        linesText.text = lines.ToString();
        if(lines % 10 == 0 && level < 10) {
            IncrementLevel();
            bonusIncrease += 10;
        }
        UpdateHighScores();
    }

    public void IncrementLevel() { 
        level++; 
        levelText.text = level.ToString();
        UpdateHighScores();
    }

    public void IncrementScore(int scoreIncreaseValue) {
        int totalIncrease = scoreIncreaseValue + bonusIncrease; 
        score += totalIncrease;
        scoreText.text = score.ToString();
        UpdateHighScores();
    }

    public int GetLines() { return lines; }

    public void UpdateHighScores() {
        if(score > PlayerPrefs.GetInt("HighScore",0)) {
            PlayerPrefs.SetInt("HighScore",score);
        }
        if(lines > PlayerPrefs.GetInt("HighLines",0)) {
            PlayerPrefs.SetInt("HighLines",lines);
        }
        if(level > PlayerPrefs.GetInt("HighLevel",0)) {
            PlayerPrefs.SetInt("HighLevel",level);
        }

        GameOverManager.finalScore = score;
        GameOverManager.finalLines = lines;
        GameOverManager.finalLevel = level;
        
        highScoreText.text = PlayerPrefs.GetInt("HighScore",0).ToString();
        highLevelText.text = PlayerPrefs.GetInt("HighLevel",0).ToString();
        highLinesText.text = PlayerPrefs.GetInt("HighLines",0).ToString();
    }
}
