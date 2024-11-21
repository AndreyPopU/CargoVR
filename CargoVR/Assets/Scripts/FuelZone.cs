using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelZone : MonoBehaviour
{
    // Gasoline pour particle system
    public ParticleSystem fuelVFX;

    private void OnTriggerStay(Collider other)
    {
        // If gas tank is inside the fuel collider
        if (other.TryGetComponent(out GasTank gasTank))
        {
            // Start playing the gasoline pour effect and filling up the tank
            if (!fuelVFX.isPlaying) fuelVFX.Play();
            gasTank.FuelUp(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If gas tank leaves the collider
        if (other.GetComponent<GasTank>())
        {
            // Stop gasoline from pouring
            if (fuelVFX.isPlaying) fuelVFX.Stop();
        }
    }
}
