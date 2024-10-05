using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // Needed for fading effect

public class VRButton : MonoBehaviour
{
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

        TakeoffMessage();
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
        }
    }

    private void TakeoffMessage() //call takeoff sequence
    {
        // Switch to takeoff cam
        if (newCamera != null && mainCamera != null)
        {
            mainCamera.enabled = false;
            newCamera.enabled = true;
        }

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