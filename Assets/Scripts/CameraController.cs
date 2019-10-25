using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// store a public reference to the Player game object, so we can refer to it's Transform
	public GameObject player;
	public float RotateSpeed = 1.2f;
	private bool playerValue ;
	public Vector3 offset;
    protected Joystick joyStick;
	private float tiltAroundZ ;
	void Start ()
	{
		offset = new Vector3(0.7f,9.14f,-10);
		transform.position = new Vector3(0.7f,9.14f,-10);
	}

	void LateUpdate ()
	{	
		playerValue = PlayerController.onInfinity;

		if(playerValue){
			Shader.SetGlobalFloat("_Curvature",2f);
			Shader.SetGlobalFloat("_Trimming",2f);
			offset = new Vector3(0f,4,-10);
			if(Input.GetAxis("Horizontal") != 0){
				 tiltAroundZ =	Input.GetAxis("Horizontal") * 10.0f;

			}else{
				 tiltAroundZ =	PlayerController.joyStick.Horizontal * 10.0f;
			}
			Quaternion target = Quaternion.Euler(0.2f, 0, tiltAroundZ);
			transform.rotation= Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5);

		}else{

			offset = new Vector3(0f,9.14f,-10);
			transform.position = new Vector3(0.7f,9.14f,-10);
			float tiltAroundZ =	Input.GetAxis("Horizontal") * 10.0f;
			Quaternion target = Quaternion.Euler(45f, 0, 0);
			transform.rotation= Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5);
		}
		if (player.transform.position.y >= -15){
			transform.position = player.transform.position+offset;
		}
	
	}
	
}