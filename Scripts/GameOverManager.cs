using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static int finalScore, finalLevel, finalLines;

    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI finalLevelText;
    [SerializeField] TextMeshProUGUI finalLinesText;

    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI highLevelText;
    [SerializeField] TextMeshProUGUI highLinesText;
    
    private void Awake() {
        SetUpScene();
    }

    private void SetUpScene() {
        finalScoreText.text = finalScore.ToString();
        finalLevelText.text = finalLevel.ToString();
        finalLinesText.text = finalLines.ToString();

        highScoreText.text = PlayerPrefs.GetInt("HighScore",0).ToString();
        highLevelText.text = PlayerPrefs.GetInt("HighLevel",0).ToString();
        highLinesText.text = PlayerPrefs.GetInt("HighLines",0).ToString();
    }
}
