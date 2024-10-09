using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SC_BigButtonInteraction : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(BigButtonGrabbed);
    }

    private void BigButtonGrabbed(SelectEnterEventArgs args)
    {
        if (VRButton.instance != null)
        {
            VRButton.instance.TakeoffMessage();
        }
        //gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(BigButtonGrabbed);
        }
    }
}
