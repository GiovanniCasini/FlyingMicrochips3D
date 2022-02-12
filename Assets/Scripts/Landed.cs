using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landed : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Movement>().enabled = false;
            other.GetComponent<Battery>().landed = true;
            other.GetComponent<Communication>().Stop();
        }
    }
}
