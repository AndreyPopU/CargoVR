using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelZone : MonoBehaviour
{
    public ParticleSystem fuelVFX;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out GasTank gasTank))
        {
            if (!fuelVFX.isPlaying) fuelVFX.Play();
            gasTank.FuelUp(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GasTank>())
        {
            if (fuelVFX.isPlaying) fuelVFX.Stop();
        }
    }
}
