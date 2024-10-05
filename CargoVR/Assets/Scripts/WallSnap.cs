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
        for (int i = 0; i < snappedCorrect.Length; i++)
        {
            if (!snappedCorrect[i]) return;

            StartCoroutine(DropPackages());
            VRButton.instance.task1 = true;
            print("Puzzle is complete");
        }
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

