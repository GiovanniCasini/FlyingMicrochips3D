using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private float batteryLevel = 0f;
    public bool readyToConnect = false;
    public bool readyToCharge = true;
    public bool landed = false;
    public float batteryToConnect = 5f;

    void Start()
    {
        InvokeRepeating("AddBattery", 60f, 0.5f);
    }

    public void AddBattery()
    {
        if (readyToCharge == true && landed == false)
        {
            batteryLevel += Random.Range(0f, 1f);
        }

        if (batteryLevel > batteryToConnect && landed == false)
        {
            readyToCharge = false;
            readyToConnect = true;
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        if(landed == true)
        {
            batteryLevel -= 0.05f;
            if(batteryLevel < batteryToConnect)
                GetComponent<Renderer>().material.color = Color.red;
        }

        if (landed == true && batteryLevel < batteryToConnect)
        {
            Stop();
        }
    }

    public void ConsumeBattery()
    {
        batteryLevel -= (batteryLevel / 1.1f);
    }

    public void Stop()
    {
        CancelInvoke("AddBattery");
    }
}
