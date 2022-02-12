using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeMovement : MonoBehaviour
{
    public float speed = 2;

    void Update()
    {
        //transform.position += transform.right * speed * Time.deltaTime;
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
