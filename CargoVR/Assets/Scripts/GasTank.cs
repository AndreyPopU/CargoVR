using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasTank : MonoBehaviour
{
    public float fuel;
    public Slider progressSlider;
    public ParticleSystem fuelVFX;

    private void OnTriggerStay(Collider other)
    {
        // if gas tank has gas inside
        if (fuel > 0)
        {
            // and is tilted down
            if (transform.eulerAngles.z > 225 && transform.eulerAngles.z < 315)
            {
                // If gasoline pour effect isn't playing - play it
                if (!fuelVFX.isPlaying) fuelVFX.Play();

                // Drain fuel
                FuelUp(-1);
            }
            else
            {
                // if isn't tilted down - stop the effect from playing
                if (fuelVFX.isPlaying) fuelVFX.Stop();
            }
        }
        else if (fuel <= 0 && fuelVFX.isPlaying) fuelVFX.Stop(); // If the gas tank gets empty and still pouring, stop pouring
    }

    public void FuelUp(float amount)
    {
        // Cap the fuel between 0 and 100
        if (fuel < -.1f) { fuel = 0; return; }
        if (fuel > 100.1f) { fuel = 100; return; }

        // Add the amount to the fuel
        fuel += amount;

        // Update fill slider
        progressSlider.value = fuel;
    }
}
