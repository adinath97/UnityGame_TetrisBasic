using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public LayerMask boundaryLayer;
    private float rayLength = 1f;
    private GameObject myParent;
    private bool done;

    private void Start() {
        myParent = this.transform.parent.gameObject;
    }
    
    void Update()
    {
        if(done) {return;}
        CheckGround();
    }

    private void CheckGround() {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, boundaryLayer);
        if(hit.collider != null) {
            done = true;
            if(myParent.tag == "BlueTetramino") {
                myParent.GetComponent<LongBlueTetramino>().StopMovement();
            }
            else if(myParent.tag == "GrayTetramino") {
                myParent.GetComponent<GrayTetramino>().StopMovement();
            }
            else if(myParent.tag == "GreenTetramino") {
                myParent.GetComponent<GreenTetramino>().StopMovement();
            }
            else if(myParent.tag == "PurpleTetramino") {
                myParent.GetComponent<PurpleTetramino>().StopMovement();
            }
            else if(myParent.tag == "RedTetramino") {
                myParent.GetComponent<RedTetramino>().StopMovement();
            }
            else if(myParent.tag == "RedTetramino2") {
                myParent.GetComponent<RedTetramino2>().StopMovement();
            }
            else if(myParent.tag == "YellowTetramino") {
                myParent.GetComponent<YellowTetromino>().StopMovement();
            }
            
        }
    }


}
