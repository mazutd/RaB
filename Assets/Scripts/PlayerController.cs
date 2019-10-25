using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;
 using UnityEngine.EventSystems;
using System.Collections;
using TMPro;
public class PlayerController : MonoBehaviour
{

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public Text countText ,winText, highScoreUI ;
    public static int playerScore;
    public TMP_Text CheckPointText;
    public GameObject exitRamp, scensManger;
    private Animator anim,anim2;
    public  Vector3 resetPosition, offset;
    public AudioClip scoreUp, damage;
    public int highScore;
    private AudioSource m_MyAudioSource;
    public static Joystick joyStick;
    public static string JoyStickAxxOption;
    public Slider speedSliderUi,volumeSliderUi;


    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    private Rigidbody rb;
    private int count;
   public static bool onInfinity;
    private float optionSpeed;
    // At the start of the game..
    void Start()
    {
        PlayerPrefs.DeleteAll();

        m_MyAudioSource = GetComponent<AudioSource>();
        volumeSliderUi.value = m_MyAudioSource.volume;
        speedSliderUi.value = speed;
        onInfinity = false;
        highScore = PlayerPrefs.GetInt("highScore");
        joyStick = FindObjectOfType<Joystick>();
        JoyStickAxxOption  = "Both";
        anim  = exitRamp.GetComponent<Animator>();
        anim2 = CheckPointText.GetComponent<Animator>();
        anim2.enabled = false;
        CheckPointText.enabled=false;

        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();
        // Set the count to zero 
        count = 0;

        // Run the SetCountText function to update the UI (see below)
        SetCountText();

        // Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winText.text = "";
        resetPlayer();

    }

    // Each physics step..
    void FixedUpdate()
    {
        highScoreUI.text = highScore.ToString();
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (onInfinity)
        {
           Camera.main.transform.position = new Vector3(0.0f, 5.0f, -10.0f);
			#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
				transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(Mathf.Clamp(gameObject.transform.position.x + moveHorizontal, -4f, 4f), gameObject.transform.position.y, gameObject.transform.position.z), 20 * Time.deltaTime);
			#endif
           GetComponent<Rigidbody>().velocity = Vector3.forward * speed * Time.deltaTime;
           //  rb.AddForce (Vector3.forward *10);


            //MOBILE CONTROLS
            #if UNITY_IPHONE || UNITY_ANDROID
            transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(Mathf.Clamp(gameObject.transform.position.x + joyStick.Horizontal,  -4f, 4f), gameObject.transform.position.y, gameObject.transform.position.z), 20 * Time.deltaTime);
            transform.Rotate(Vector3.right * GetComponent<Rigidbody>().velocity.z / 3);
            #endif
            
        }
        else
        {
            onInfinity = false;
            #if UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE || UNITY_EDITOR
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
            #endif
            #if UNITY_IPHONE || UNITY_ANDROID
                rb.velocity = new Vector3(joyStick.Horizontal*speed/95f, rb.velocity.y,joyStick.Vertical*speed/95f);
            #endif

            if (transform.position.y <= -60)
            {
                resetPlayer();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "CheckPoint")
        {
                CheckPointText.enabled=true;
                resetPosition = transform.position;
                anim2.enabled = true;
                anim2.Play("fadeIn",  -1, 0f);


        }
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<AudioSource>().PlayOneShot(damage, 1.0f);
            scensManger.GetComponent<App_Inis>().gameOver();
        }
        if (other.gameObject.tag == "InfinityMapp")
        {
            if(!onInfinity){
            JoyStickAxxOption  = "onlyHorzintal";
            onInfinity = true;
            }
        }
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;
            if(count>highScore){
                highScore = count;
                PlayerPrefs.SetInt("highScore",highScore);
            }
            GetComponent<AudioSource>().PlayOneShot(scoreUp, 1.0f);
            SetCountText();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SpeedPipe")
        {
            JoyStickAxxOption  = "onlyHorzintal";
            Boots();

        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            StartCoroutine(Example());
        }
      
        if (other.gameObject.tag == "SpeedPipe")
        {
            JoyStickAxxOption  = "Both";
            GetComponent<Rigidbody>().velocity = Vector3.forward * 20 * 0.081f;
        }
    }
    private void Boots()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.forward * 250 * 0.081f;
        rb.AddForce (Vector3.forward * 2 * 300);


    }

    private void BootsPlayer()
    {

    }

    void resetPlayer()
    {
        onInfinity = false;
        transform.position = resetPosition;
        JoyStickAxxOption  = "Both";

    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    void SetCountText()
    {
        // Update the text field of our 'countText' variable
        countText.text = count.ToString();
        //Send Score to  ObjectSpwaner(Script)
        playerScore = count;
        // Check if our 'count' is equal to or exceeded 12
        if (count >= 10)
        {
            anim.enabled = true;
            anim.Play("slidingDown");

            
        }
    }
    public void speedSlider(float optionSpeed){
        speed=optionSpeed;
    }
       public void volumeSlider(float voulmeValue){
        m_MyAudioSource.volume=voulmeValue;
    }

        IEnumerator Example()
    {
        yield return new WaitForSeconds(3);
        anim2.enabled = false;
        CheckPointText.enabled=false;
    }

}