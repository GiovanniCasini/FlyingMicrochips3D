using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    public Manager manager;
    private bool justOnce = true;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            manager.microchipsHarvested[collision.GetComponent<Communication>().id] = collision.gameObject;
            collision.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKey("h") && justOnce == true)
        {
            justOnce = false;
            Destroy(gameObject);
        }
    }
}
