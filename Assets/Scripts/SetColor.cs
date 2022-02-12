using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    public int id;
    private Manager manager;
    public int count = 0;

    void Start()
    {
        manager = FindObjectOfType<Manager>();
        for (int i = 0; i < manager.numMicrochips; i++)
        {
            if (manager.datasHarvested[i] != -1)
            {
                if (Vector3.Distance(transform.position, manager.tests[i].transform.position) < 75f)
                {
                    count++;
                }
            }
        }
        if(count > 8)
            GetComponent<Renderer>().material.color = Color.green;
        else if (count > 1)
            GetComponent<Renderer>().material.color = Color.yellow;
        else
            GetComponent<Renderer>().material.color = Color.red;
    }
}
