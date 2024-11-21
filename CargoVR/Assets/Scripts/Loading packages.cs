using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadingpackages : MonoBehaviour
{
    int packagenumber = 0; //keeps track of the amount of packages loaded on ship
  void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Packages")
        {
            other.gameObject.SetActive(false); //once packages hit the collider they dissapear
            packagenumber++; //increase the number of packages already loaded on ship
        }
        if (packagenumber == 4)
        {
            gameObject.SetActive (false); //once all 5 packages are on board the hitbox is no longer functional
            VRButton.instance.task4 = true;
            //Destroy (Object Hitbox); //alternatively destroy the hitbox
        }
    }
}
