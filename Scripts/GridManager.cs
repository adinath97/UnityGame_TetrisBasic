using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GameObject[,] gameGrid = new GameObject[10,25];

    private ScoreManager scoreManager;

    private GameManager gameManager;

    private ShapeInstantiator myShapeInstantiator;

    private int scoreIncreaseValue = 10;

    private bool gameOver;

    private void Awake() {
        SetUp();
    }

    private void SetUp() {
        //reset gameGrid for new level/game
        this.scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        this.gameManager = GameObject.FindObjectOfType<GameManager>();
        myShapeInstantiator = GameObject.FindObjectOfType<ShapeInstantiator>();
    }

    public void CheckForLines() {
        bool rowEligible = false;
        for(int j = 0; j < 20; j++) {
            rowEligible = true;
            for(int i = 0; i < 10; i++) {
                if(gameGrid[i,j] == null) {
                    rowEligible = false;
                }
                if(rowEligible && i == 9) {
                    ClearLine(j);
                    CheckForLines();
                }
            }
        }
    }

    public void CheckIfGameOver() {
        for(int j = 0; j < 20; j++) {
            for(int i = 0; i < 10; i++) {
                if(gameGrid[i,j] != null) {
                    if(j == 19) { 
                        gameManager.GameOver();
                    }
                    break;
                }
            }
        }
        if(!gameManager.CheckIfGameOver()) {
            myShapeInstantiator.InstantiateNow();
        }
    }

    private void ClearLine(int x) {
        gameManager.LineClearSFX();
        for(int i = 0; i < 10; i++) {
            Destroy(gameGrid[i,x].gameObject);
            gameGrid[i,x] = null;
        }
        //don't move everything down one
        //move down from above the row that was completed
        MoveRowDown(x);
        scoreManager.IncrementLines();
        scoreManager.IncrementScore(scoreIncreaseValue);
    }

    public static void MoveRowDown(int rowToDelete) {
        for(int j = rowToDelete + 1; j < 20; j++) {
            for(int i = 0; i < 10; i++) {
                if(gameGrid[i,j] != null) {
                    gameGrid[i,j].transform.position = new Vector3(gameGrid[i,j].transform.position.x, gameGrid[i,j].transform.position.y - 1, gameGrid[i,j].transform.position.z);
                }
                gameGrid[i,j - 1] = gameGrid[i,j];
            }
        }
    }
}
