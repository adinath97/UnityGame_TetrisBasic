using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayTetramino : MonoBehaviour
{
    [SerializeField] ShapeInstantiator shapeInstantiator;

    private ScoreManager scoreManager;

    private GridManager gridManagerInstance;

    private GameManager gameManager;
    
    public Vector3 rotationPoint;
    
    private float verticalStep = -1f, fallSpeed = 1f, fallTimer = 0, sideSpeed = .1f, sideTimer, rightKeyWaitTimer, leftKeyWaitTimer;

    private int width = 10, height = 25, scoreIncreaseValue = 10;

    private bool stopMovement, nameChangeDone;

    private KeyCode latestKey, previousKey;

    private void Awake() {
        SetUp();
    }

    private void Update() {
        UpdateNames();
        if(this.stopMovement) {return;}
        GetVerticalUserInput();
        GetHorizontalUserInput();
        GetRotationalInput();
        GetLatestKeyPressed();
    }

    private void GetLatestKeyPressed() {
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            previousKey = KeyCode.LeftArrow;
            latestKey = KeyCode.RightArrow; 
        }
 
        else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            previousKey = KeyCode.RightArrow;
            latestKey = KeyCode.LeftArrow; 
        }

        if (Input.GetKey(latestKey)) {
            ContinuousHorizontalMovement();
        }

        else {
            if(Input.GetKey(previousKey)) {
                ContinuousHorizontalMovement();
            }
        }
    }

    private void ContinuousHorizontalMovement() {
        if(previousKey == KeyCode.RightArrow) {
            rightKeyWaitTimer += Time.deltaTime;
            if(rightKeyWaitTimer > .2f) {
                this.transform.position += new Vector3(1f,0,0);
                if(!ValidMove()) {
                    this.transform.position += new Vector3(-1f,0,0);
                }
                rightKeyWaitTimer = 0;
            }
        }
        else {
            leftKeyWaitTimer += Time.deltaTime;
            if(leftKeyWaitTimer > .2f) {
                this.transform.position += new Vector3(-1f,0,0);
                if(!ValidMove()) {
                    this.transform.position += new Vector3(1f,0,0);
                }
                leftKeyWaitTimer = 0;
            }
        }
    }

    private void UpdateNames() {
        foreach(Transform children in this.transform) {
            children.gameObject.name = this.gameObject.name;
        }
        if(this.gameObject.name != ShapeInstantiator.counter.ToString() && !nameChangeDone) {
            nameChangeDone = true;
            this.gameObject.name = ShapeInstantiator.counter.ToString();
            ShapeInstantiator.counter++;
        }
        gridManagerInstance = GameObject.FindObjectOfType<GridManager>();
    }

    private void SetUp() {
        this.scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        fallSpeed = 1f;
        if(scoreManager.GetLines() > 100) {
            fallSpeed = .1f;
            scoreIncreaseValue = 55;
        }
        else if(scoreManager.GetLines() > 90) {
            fallSpeed = .2f;
            scoreIncreaseValue = 50;
        }
        else if(scoreManager.GetLines() > 80) {
            fallSpeed = .3f;
            scoreIncreaseValue = 45;
        }
        else if(scoreManager.GetLines() > 70) {
            fallSpeed = .4f;
            scoreIncreaseValue = 40;
        }
        else if(scoreManager.GetLines() > 60) {
            fallSpeed = .5f;
            scoreIncreaseValue = 35;
        }
        else if(scoreManager.GetLines() > 50) {
            fallSpeed = .6f;
            scoreIncreaseValue = 30;
        }
        else if(scoreManager.GetLines() > 40) {
            fallSpeed = .7f;
            scoreIncreaseValue = 25;
        }
        else if(scoreManager.GetLines() > 30) {
            fallSpeed = .8f;
            scoreIncreaseValue = 20;
        }
        else if(scoreManager.GetLines() > 20) {
            fallSpeed = .9f;
            scoreIncreaseValue = 15;
        }
        this.gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void GetRotationalInput() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if(!ValidMove()) {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
            }
        }
    }

    private bool ValidMove() {
        foreach(Transform children in this.transform) {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height) { return false; }

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(roundedX,roundedY), .1f);
            
            foreach(var hitCollider in hitColliders)
            {
                if(hitCollider.gameObject.name != this.gameObject.name 
                    && hitCollider.gameObject.name != "LeftWall"
                    && hitCollider.gameObject.name != "RightWall"
                    && hitCollider.gameObject.name != "BottomWall"
                    && hitCollider.gameObject.name != "GrayTetramino(Clone)") {
                    return false;
                }
            }
        }
        return true;
    }

    private void GetHorizontalUserInput() {
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            this.transform.position += new Vector3(1f,0,0);
            leftKeyWaitTimer = 0;
            rightKeyWaitTimer = 0;
            if(!ValidMove()) {
                this.transform.position += new Vector3(-1f,0,0);
            }
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            this.transform.position += new Vector3(-1f,0,0);
            leftKeyWaitTimer = 0;
            rightKeyWaitTimer = 0;
            if(!ValidMove()) {
                this.transform.position += new Vector3(1f,0,0);
            }
            return;
        }
    }

    private void UpdateGameGrid() {
        foreach(Transform child in this.transform) {
            if(Mathf.RoundToInt(child.position.x) >= 10 || Mathf.RoundToInt(child.position.y) >= 20) {continue;}
            GridManager.gameGrid[Mathf.RoundToInt(child.position.x),Mathf.RoundToInt(child.position.y)] = child.gameObject;
        }
        this.gridManagerInstance.CheckForLines();
        this.scoreManager.IncrementScore(scoreIncreaseValue);
        this.gridManagerInstance.CheckIfGameOver();
    }

    private void GetVerticalUserInput() {
        fallSpeed = 1f;
        if(Input.GetKey(KeyCode.DownArrow)) {
            fallSpeed = .2f;
            if(scoreManager.GetLines() > 70) {
                fallSpeed = .075f;
            }
        }
        if(Time.time - fallTimer >= fallSpeed) {
            this.transform.position += new Vector3(0,verticalStep,0);
            if(!ValidMove()) {
                this.transform.position += new Vector3(0,-verticalStep,0);
                verticalStep = 0;
            }
            else {
                verticalStep = -1f;
            }
            fallTimer = Time.time;
        }
    }

    public void StopMovement() {
        if(!stopMovement) {
            this.stopMovement = true;
            UpdateGameGrid();
            foreach(Transform child in transform) {
                child.gameObject.layer = LayerMask.NameToLayer("Boundary");
            }
        }
    }
}
