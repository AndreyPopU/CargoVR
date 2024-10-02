using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ShipMovement : MonoBehaviour
{
    public float moveSpeed;
    public float lifetime; //disappear
    public float cooldown = 2f;      //reappear

    private Vector3 startPosition;   
    private Renderer objRenderer;    

    void Start()
    {
        moveSpeed = Random.Range(2f, 15f); //set random speed
        lifetime = Random.Range(5f, 15f); //set random lifetime

        startPosition = transform.position;        // save starting point

        objRenderer = GetComponent<Renderer>();

        StartCoroutine(DisappearAndRespawn());
    }

    void Update()
    {
        // Move the object forward (vector down is used because of the rotation of the object)
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    IEnumerator DisappearAndRespawn()
    {
        yield return new WaitForSeconds(lifetime);
        objRenderer.enabled = false;
        yield return new WaitForSeconds(cooldown);
        transform.position = startPosition;
        objRenderer.enabled = true;
        StartCoroutine(DisappearAndRespawn());

        moveSpeed = Random.Range(2f, 15f); //change random speed for "new" ship
        lifetime = Random.Range(5f, 15f); //set random lifetime
    }
}
