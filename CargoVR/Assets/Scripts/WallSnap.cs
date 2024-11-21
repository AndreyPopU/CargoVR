using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WallSnap : MonoBehaviour
{
    public static WallSnap instance;

    public int snappedCount;
    public bool[] snappedCorrect = new bool[3];

    [Header("Packages")]
    public GameObject[] packagePrefabs;
    public Transform spawnPosition;
    public ParticleSystem spawnEffect;

    private void Awake()
    {
        instance = this;
    }

    public void CheckIfPipeCorrect()
    {
        // If complete - return;
        if (VRButton.instance.task1) return;

        for (int i = 0; i < snappedCorrect.Length; i++) // Go through whole snapped bool array
        {
            if (!snappedCorrect[i]) return; // If a bool is false - not all pipes are snapped correctly - return
        }

        // Start dropping packages
        StartCoroutine(DropPackages());

        // Set puzzle as complete
        VRButton.instance.task1 = true;
    }

    private IEnumerator DropPackages()
    {
        float interval = 1;
        int packages = 4;

        while(packages > 0)
        {
            Instantiate(packagePrefabs[--packages], spawnPosition.position, Quaternion.identity);
            spawnEffect.Play();
            yield return new WaitForSeconds(interval);
        }
    }
 }

