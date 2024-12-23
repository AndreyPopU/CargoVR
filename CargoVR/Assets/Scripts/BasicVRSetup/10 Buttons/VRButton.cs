using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // Needed for fading effect
using UnityEngine.XR.Interaction.Toolkit;

public class VRButton : MonoBehaviour
{
    public Transform PlayerObject;
    //taskrefs
    public TMP_Text textMeshPro1;
    public TMP_Text textMeshPro2;
    public TMP_Text textMeshPro3;
    public TMP_Text textMeshPro4;
    public TMP_Text textMeshPro5;

    public TMP_Text congrats; //end game texts

    public static VRButton instance;

    // Time that the button is set inactive after release
    public float deadTime = 1.0f;
    // Bool used to lock down button during its set dead time
    private bool _deadTimeActive = false;

    // Public Unity Events we can use in the editor and tie other functions to.
    public UnityEvent onPressed, onReleased;

    // cameras
    public Camera newCamera;
    private Camera mainCamera;

    // Cargo ship taking off
    public GameObject cargoShip;
    public string cargoShipFunction;

    //Hangar roof opening
    public GameObject hangarRoof;
    public string hangarRoofFunction;

    // Time to wait before camera fadeout
    public float fadeDelay = 3.0f;
    // UI 
    public Image fadeOverlay;
    public float fadeDuration = 2.0f;

    public bool task1 = false; //pipes
    public bool task2 = false; //gas canister
    public bool task3 = false; //helmet
    public bool task4 = false; //packages on landing pad
    public bool task5 = false; //condensed fuel

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main; //check main camera
        if (fadeOverlay != null)
        {
            fadeOverlay.color = new Color(0, 0, 0, 0); // overlay starts transparent check
        }

        //TakeoffMessage(); //takeoff sequence testing
    }

    // Checks if the current collider entering is the Button and sets off OnPressed event.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive && task1 && task2 && task3 && task4 && task5)
        {
            onPressed?.Invoke();
            Debug.Log("I have been pressed");

            TakeoffMessage();
        }
    }

    // Checks if the current collider exiting is the Button and sets off OnReleased event.
    // It will also call a Coroutine to make the button inactive for however long deadTime is set to.
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onReleased?.Invoke();
            Debug.Log("I have been released");
            StartCoroutine(WaitForDeadTime());
        }
    }

    // Locks button activity until deadTime has passed and reactivates button activity.
    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }

    // Fades the screen to black after a delay
    IEnumerator FadeToBlackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (fadeOverlay != null)
        {
            float elapsedTime = 0;
            Color startColor = fadeOverlay.color;
            Color endColor = new Color(0, 0, 0, 1); // full black

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                fadeOverlay.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
                yield return null;
            }

            fadeOverlay.color = endColor; // fully black at end check
            congrats.color = new Color(1, 1, 1, 1); //make congrats message appear when screen has fully faded
        }

        
    }

    public void TakeoffMessage() //call takeoff sequence
    {
        // Switch to takeoff cam
        if (newCamera != null && mainCamera != null && task1 && task2 && task3 && task4 && task5)
        {
            //mainCamera.enabled = false;
            //newCamera.enabled = true;

            //instead of switching cameras move player to a position and rotate the camera
            PlayerObject.position = newCamera.transform.position;
            mainCamera.transform.rotation = newCamera.transform.rotation;

            // call takeoff function on cargo ship
            if (cargoShip != null && !string.IsNullOrEmpty(cargoShipFunction))
            {
                cargoShip.SendMessage(cargoShipFunction);
            }
            // call takeoff for hangar roof
            if (hangarRoof != null && !string.IsNullOrEmpty(hangarRoofFunction))
            {
                hangarRoof.SendMessage(hangarRoofFunction);
            }
            // start fade after delay
            StartCoroutine(FadeToBlackAfterDelay(fadeDelay));
        }
    }
    void Update()
    {
        //change colour to green if value is true
        if (task1)
        {
            textMeshPro1.color = Color.green;
        }
        if (task2)
        {
            textMeshPro2.color = Color.green;
        }
        if (task3)
        {
            textMeshPro3.color = Color.green;
        }
        if (task4)
        {
            textMeshPro4.color = Color.green;
        }
        if (task5)
        {
            textMeshPro5.color = Color.green;
        }
    }
}