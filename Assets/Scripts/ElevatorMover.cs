using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMover : MonoBehaviour
{
    public bool elevatorMoving = false;
    public GameObject elevator;
    public GameObject frontWall;
    public GameObject player;
    void FixedUpdate()
    {
        if (elevatorMoving == true)
        {
            
            if (elevator.transform.position.y <=28f)
            {
                elevator.transform.position = new Vector3(elevator.transform.position.x, elevator.transform.position.y +0.1f, elevator.transform.position.z);
            }
            else
            {
                elevatorMoving = false;
                frontWall.SetActive(false);
            }
        }        
    }
     void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "Player")
        {
            elevatorMoving = true;
        }
    }

     void OnCollisionExit(Collision collisionInfo)
    {
        if(collisionInfo.collider.gameObject.tag == "Player"){
        }
    }
}
