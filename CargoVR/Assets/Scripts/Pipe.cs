using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pipe : MonoBehaviour
{
    public enum Type { Straight, Round, Cross, Tshape }

    public Type type;
    public Vector3[] desiredRotations;
    [HideInInspector] public int typeIndex;

    private void Start()
    {
        switch (type)
        {
            case Type.Straight: typeIndex = 0; break;
            case Type.Round: typeIndex = 1; break;
            case Type.Cross: typeIndex = 2; break;
            case Type.Tshape: typeIndex = 3; break;
        }
    }

    public Quaternion SnapToDesire()
    {
        float closest = 999;
        int index = 0;

        // Loop through possible rotations and select the one closest to current rotation
        for (int i = 0; i < desiredRotations.Length; i++)
        {
            float difference = Quaternion.Angle(transform.rotation, Quaternion.Euler(desiredRotations[i]));

            if (difference < closest) 
            {
                closest = difference;
                index = i;
            }
        }

        return Quaternion.Euler(desiredRotations[index]);
    }
}
