using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfuelZone : MonoBehaviour
{
    public float value;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out GasTank gasTank))
        {
            if (gasTank.transform.eulerAngles.z > 225 && gasTank.transform.eulerAngles.z < 315)
            {
                value++;

                if (value >= 20)
                {
                    if (gasTank.fuelVFX.isPlaying) gasTank.fuelVFX.Stop();
                    VRButton.instance.task2 = true;
                }
            }
        }
    }
}
