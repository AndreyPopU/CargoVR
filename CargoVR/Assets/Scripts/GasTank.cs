using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasTank : MonoBehaviour
{
    public float value;
    float EngineValue;
    public Slider progressSlider;
    public ParticleSystem fuelVFX;

    private void OnTriggerStay(Collider other)
    {
        if (value > 0)
        {
            if (transform.eulerAngles.z > 225 && transform.eulerAngles.z < 315)
            {
                if (!fuelVFX.isPlaying) fuelVFX.Play();
                FuelUp(-1);
                EngineValue = EngineValue + 1;
            }
            else
            {
                if (fuelVFX.isPlaying) fuelVFX.Stop();
            }
        }
        else if (value <= 0 && fuelVFX.isPlaying) fuelVFX.Stop();
    }
    public void FuelUp(float amount)
    {
        if (value < -.1f) { value = 0; return; }
        if (value > 100.1f) { value = 100; return; }

        value += amount;
        progressSlider.value = value;
    }
}
