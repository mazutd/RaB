using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class ObjectSpwaner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] rampsPrefabs; 
    public GameObject[] infinityRamps; 
    private Vector3 spanObstaclePosition; 
    private GameObject lastRamp;
    public Vector3 firstRamp; 
    
	private static int score;
    private bool infinityStart = false;
    public float maxScoreBeforeInfinity ;
    private float lastOneBeforeInfinity;
    private float lastLevel = 2;
    private void Start() {
        lastOneBeforeInfinity = 0;
    }
    // Update is called once per frame
    void Update()
    {
    score = PlayerController.playerScore;
    float distansToHorz = Vector3.Distance(player.gameObject.transform.position, spanObstaclePosition);    

    
    if(score < maxScoreBeforeInfinity){
        if(distansToHorz < 150){
            SpwanRamps();
        }
    }

    if(score == lastLevel*maxScoreBeforeInfinity){
            createCheckPointInsideInfinity();
        }

    //när score är lika med Nästalevel då skapas infinity ramp hela så länge det är true
    else if(score==maxScoreBeforeInfinity) {
        infinityStart = true;
        lastOneBeforeInfinity = 1;
    }
    if(infinityStart){
        if(distansToHorz < 50){
            SpwanRampsWining();
        }
    }
    }



    void createCheckPointInsideInfinity(){
        lastLevel++;
        lastRamp = infinityRamps[infinityRamps.Length-1];
        firstRamp.z = firstRamp.z+30;
        spanObstaclePosition = new Vector3(firstRamp.x,28f,firstRamp.z);
        Instantiate(lastRamp, spanObstaclePosition, Quaternion.identity);
        PlayerController pll = player.GetComponent<PlayerController>();
        pll.speed = pll.speed+100;
    }

    void SpwanRamps(){
        lastRamp = rampsPrefabs[(Random.Range(0,rampsPrefabs.Length-1))];
        firstRamp.z = firstRamp.z+30;
         
        spanObstaclePosition = new Vector3(firstRamp.x,firstRamp.y,firstRamp.z);
        Instantiate(lastRamp, spanObstaclePosition, Quaternion.identity);
    }

        void SpwanRampsWining(){
        if(lastOneBeforeInfinity == 1){
            lastRamp = rampsPrefabs[rampsPrefabs.Length-1];
            firstRamp.z = firstRamp.z+30;
            lastOneBeforeInfinity++;
            spanObstaclePosition = new Vector3(firstRamp.x,firstRamp.y,firstRamp.z);
            Instantiate(lastRamp, spanObstaclePosition, Quaternion.identity);
        }
                    Debug.Log("Score = "+score+" lastlevel="+lastLevel*maxScoreBeforeInfinity);


        CancelInvoke("SpwanRamps");
        lastRamp = infinityRamps[(Random.Range(0,infinityRamps.Length-2))];
        firstRamp.z = firstRamp.z+30;
        spanObstaclePosition = new Vector3(firstRamp.x,28f,firstRamp.z);
        Instantiate(lastRamp, spanObstaclePosition, Quaternion.identity);
    }



}
