using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LoadGameScene() {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadStartMenu() {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadGameOverScene() {
        SceneManager.LoadScene("GameOverScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
