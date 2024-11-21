using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FuelTank : MonoBehaviour
{
    public float value;

    private GasTank gasTank;

    private void Start()
    {
        gasTank = FindObjectOfType<GasTank>();
    }

    private void OnTriggerStay(Collider other)
    {
        // If gas tank above fuel zone and tilted down - start fueling
        if (gasTank.fuel > 0 && gasTank.transform.eulerAngles.z > 225 && gasTank.transform.eulerAngles.z < 315)
            FuelUp(1);
    }

    public void FuelUp(float amount)
    {
        if (value > 97) 
        {
            VRButton.instance.task2 = true;
            value = 100;
            return;
        }

        value += amount;
    }
}
