using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject threeBox;
    [SerializeField] GameObject twoBox;
    [SerializeField] GameObject oneBox;
    [SerializeField] GameObject beginBox;

    [SerializeField] AudioClip[] countDownSounds;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip lineClearSound;

    private AudioSource myAudioSource;
    private bool gameOver;

    private void Awake() {
        SetUp();
    }

    private void SetUp() {
        twoBox.SetActive(false);    
        oneBox.SetActive(false);
        beginBox.SetActive(false);
        myAudioSource = this.GetComponent<AudioSource>();
        StartCoroutine(CountDownRoutine());
    }

    private IEnumerator CountDownRoutine() {
        myAudioSource.PlayOneShot(countDownSounds[0]);
        threeBox.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        myAudioSource.PlayOneShot(countDownSounds[0]);
        threeBox.SetActive(false);
        twoBox.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        myAudioSource.PlayOneShot(countDownSounds[0]);
        twoBox.SetActive(false);
        oneBox.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        myAudioSource.PlayOneShot(countDownSounds[1]);
        oneBox.SetActive(false);
        beginBox.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        beginBox.SetActive(false);
        ShapeInstantiator myShapeInstantiator = GameObject.FindObjectOfType<ShapeInstantiator>();
        myShapeInstantiator.BeginGame();
    }
    
    public void GameOver() {
        myAudioSource.PlayOneShot(gameOverSound);
        gameOver = true;
        SceneManagerScript mySceneManagerScript = GameObject.FindObjectOfType<SceneManagerScript>();
        mySceneManagerScript.GameSceneOver();
    }

    public bool CheckIfGameOver() {
        return gameOver;
    }

    public void LineClearSFX() {
        myAudioSource.PlayOneShot(lineClearSound);
    }
}
