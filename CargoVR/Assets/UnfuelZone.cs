using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfuelZone : MonoBehaviour
{
    public float value;

    private void OnTriggerStay(Collider other)
    {
        // If gas tank is in the collider
        if (other.TryGetComponent(out GasTank gasTank))
        {
            // And said gas tank is tilted 45 degrees
            if (gasTank.transform.eulerAngles.z > 225 && gasTank.transform.eulerAngles.z < 315)
            {
                // Fill up fuel
                value++;

                // If at least 20 fuel has been fueled - mark the minigame as complete and stop pouring gas
                if (value >= 20)
                {
                    if (gasTank.fuelVFX.isPlaying) gasTank.fuelVFX.Stop();
                    VRButton.instance.task2 = true;
                }
            }
        }
    }
}
