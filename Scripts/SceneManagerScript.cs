using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] GameObject startFade;
    [SerializeField] GameObject endFade;

    [SerializeField] AudioClip clickSFX;

    private AudioSource myAudioSource;

    private bool gameOver;

    private void Awake() {
        StartCoroutine(StartRoutine());
    }

    private void Update() {
        CheckIfGameOver();
    }

    private void CheckIfGameOver() {
        if(SceneManager.GetActiveScene().name == "GameScene" && gameOver) {
            gameOver = false;
            LoadGameOverScene();
        }
    }

    public void GameSceneOver() {
        gameOver = true;
    }

    private IEnumerator StartRoutine() {
        endFade.SetActive(false);
        startFade.SetActive(true);
        myAudioSource = this.GetComponent<AudioSource>();
        yield return new WaitForSeconds(.5f);
        startFade.SetActive(false);
    }
    
    public void LoadGameScene() {
        StartCoroutine(GameRoutine());
    }

    private IEnumerator GameRoutine() {
        myAudioSource.PlayOneShot(clickSFX);
        endFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGameOverScene() {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine() {
        yield return new WaitForSeconds(.5f);
        endFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("GameOverScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
