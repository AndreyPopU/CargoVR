using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SC_LockerDoor : MonoBehaviour
{
    //copied from vr button
    public float deadTime = 1.0f;
    private bool _deadTimeActive = false;
    public UnityEvent onPressed, onReleased;

    // Variables for door movement
    public Transform doorTransform; // The pivot point of the door
    public Vector3 openRotation;
    public Vector3 closedRotation;
    public float rotationSpeed = 2.0f;

    private bool isOpen = false;

    void Start()
    {
        //start w closed door
        doorTransform.localRotation = Quaternion.Euler(closedRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("I have been pressed");

            // Toggle door state (open/close)
            if (isOpen)
            {
                StartCoroutine(RotateDoor(closedRotation)); // close door
            }
            else
            {
                StartCoroutine(RotateDoor(openRotation)); // open door
            }

            isOpen = !isOpen; // toggle state
        }
    }

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

    // smooth rotation
    IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
        while (Quaternion.Angle(doorTransform.localRotation, targetQuaternion) > 0.1f)
        {
            doorTransform.localRotation = Quaternion.Slerp(doorTransform.localRotation, targetQuaternion, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // target location check
        doorTransform.localRotation = targetQuaternion;
    }
}