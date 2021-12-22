using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeInstantiator : MonoBehaviour
{
    [SerializeField] List<GameObject> tetraminos;
    [SerializeField] List<Sprite> previewTetraminos;

    private GameManager gameManager;

    private GameObject previewTetramino, instantiationPoint;

    public static int counter, randX;

    private bool startGame;

    private void Update() {
        BeginInstantiation();
    }

    public void BeginInstantiation() {
        if(startGame) {
            startGame = false;
            InstantiateFirstTetramino();
        }
    }

    public void BeginGame() {
        startGame = true;
    }

    private void InstantiateFirstTetramino() {
        instantiationPoint = GameObject.Find("InstantiationPoint");
        previewTetramino = GameObject.Find("PreviewTetramino");
        randX = Mathf.RoundToInt(Random.Range(0,tetraminos.Count));
        counter = 0;
        Instantiate(tetraminos[randX],instantiationPoint.transform.position,Quaternion.identity);
        randX = Mathf.RoundToInt(Random.Range(0,tetraminos.Count));
        DisplayNextTetramino();
    }

    public void InstantiateNow() {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if(gameManager.CheckIfGameOver()) {return;}
        instantiationPoint = GameObject.Find("InstantiationPoint");
        Instantiate(tetraminos[randX],instantiationPoint.transform.position,Quaternion.identity);
        randX = Mathf.RoundToInt(Random.Range(0,tetraminos.Count));
        DisplayNextTetramino();
    }

    public void DisplayNextTetramino() {
        previewTetramino = GameObject.Find("PreviewTetramino");
        previewTetramino.GetComponent<SpriteRenderer>().sprite = previewTetraminos[randX];
    }
}
