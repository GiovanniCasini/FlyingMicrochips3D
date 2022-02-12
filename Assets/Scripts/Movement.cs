using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private float directionChangeTime = 3f;
    public float characterVelocity = 4f;

    private Vector3 moveTo;
    private Vector3 newVector;

    void Start()
    {
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }

    void calcuateNewMovementVector()
    {
        float x = transform.position.x + Random.Range(-5f, 5f);
        float y = transform.position.y + Random.Range(-5f, 0f);
        float z = transform.position.z + Random.Range(5f, 20f);
        newVector.x = x;
        newVector.y = y;
        newVector.z= z;

        moveTo = newVector;
        directionChangeTime = Random.Range(1.5f, 4.0f);
    }

    void Update()
    {
        if (Time.time - latestDirectionChangeTime > directionChangeTime || transform.position == moveTo)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        transform.position = Vector3.MoveTowards(transform.position, moveTo, characterVelocity * Time.deltaTime);
    }
}
