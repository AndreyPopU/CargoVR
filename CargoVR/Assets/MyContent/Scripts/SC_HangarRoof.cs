using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_HangarRoof : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    public bool takingOff = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (takingOff)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    public void Takeoff()
    {
        takingOff = true;
    }
}
