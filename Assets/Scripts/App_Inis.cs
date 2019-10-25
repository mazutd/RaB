using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class App_Inis : MonoBehaviour
{
    public GameObject inMenuUi;
    public GameObject inGameUI;
    public GameObject gameOverUI;
    public GameObject optionsUI;
    public GameObject player;
    public GameObject settings;
    private bool hasGameStarted;
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        // Delets  alla Svaed HighScore
        settings.transform.localScale = new Vector3(0, 0, 0);
        inMenuUi.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);

        
    }
    public void PlayButton () {Debug.Log("sf");
        if(hasGameStarted == true){
            StartCoroutine(StartGame(1.0f));
        }else{
            StartCoroutine(StartGame(0.0f));
        }
        

    }
    public void restartGame(){
         SceneManager.LoadScene(0); //loads whatver scene is at index 0 in build settings

    }

    public void optionsMenuUI (){
        settings.transform.localScale = new Vector3(1,1,1);

    }


    public void showAds(){
        StartCoroutine(StartGame(1.0f));
        PlayerController pll = player.GetComponent<PlayerController>();
        player.GetComponent<Rigidbody>().transform.position = pll.resetPosition;
        
    }
    public void QuitGame(){
        Application.Quit();
    }
        public void gameOver (){
        settings.transform.localScale = new Vector3(0, 0, 0);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        hasGameStarted = true;
        inMenuUi.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);

    }
    public void PausGame () {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        hasGameStarted = true;
        
        inMenuUi.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
    }
    // StartGame onclick 
    IEnumerator StartGame(float waitTime) {
        settings.transform.localScale = new Vector3(0, 0, 0);
        inMenuUi.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }
}
