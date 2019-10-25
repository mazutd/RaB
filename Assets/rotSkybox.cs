using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotSkybox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		RenderSettings.skybox.SetFloat("_Rotation", Time.time);
        Debug.Log("skybox");        
    }
}
