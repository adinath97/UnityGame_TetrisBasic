using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameOver;
    
    public void GameOver() {
        gameOver = true;
        StartCoroutine(WaitAndLoadGameOver());
    }

    public bool CheckIfGameOver() {
        return gameOver;
    }

    private IEnumerator WaitAndLoadGameOver() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOverScene");
    }
}
