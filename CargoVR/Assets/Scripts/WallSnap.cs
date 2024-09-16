using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WallSnap : MonoBehaviour
{
    public static WallSnap instance;

    public int snappedCount;
    public bool[] snappedCorrect = new bool[3];

    private void Awake()
    {
        instance = this;
    }

    public void CheckIfPipeCorrect()
    {
        for (int i = 0; i < snappedCorrect.Length; i++)
        {
            if (!snappedCorrect[i]) return;

            print("Puzzle is complete");
        }
    }
 }

