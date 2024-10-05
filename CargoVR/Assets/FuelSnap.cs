using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FuelSnap : MonoBehaviour
{
    private XRSocketInteractor interactor;

    void Start()
    {
        interactor = GetComponent<XRSocketInteractor>();
    }

    public void OnSnapFuel() // Invoked in Inspector
    {
        if (interactor.GetOldestInteractableSelected() != null && interactor.GetOldestInteractableSelected().transform.tag == "CondensedFuel")
            VRButton.instance.task5 = true;
    }

    public void RemoveFuel()
    {
        // Change puzzle to false
        VRButton.instance.task5 = false;
    }
}
