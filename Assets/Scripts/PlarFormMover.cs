using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlarFormMover : MonoBehaviour
{
    public float speed = 3;
    private Vector3 startingPos;
    public float direction;
    public float distanse;
    public string axis;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(axis == "x"){
        transform.position = new Vector3(direction*Mathf.PingPong(Time.time*speed, distanse) + startingPos.x, transform.position.y,transform.position.z);
        }
        if(axis == "y"){
        transform.position = new Vector3(transform.position.x, direction*Mathf.PingPong(Time.time, distanse)+startingPos.y, transform.position.z);
        }
        if(axis == "z"){
        transform.position = new Vector3(transform.position.x, transform.position.y, direction*Mathf.PingPong(Time.time*speed, distanse)+startingPos.z);
        }
    }
}
