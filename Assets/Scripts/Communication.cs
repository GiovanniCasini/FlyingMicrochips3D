using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Communication : MonoBehaviour
{
    public Manager manager;
    private float timeSearch = 0.3f;
    private float drop;

    public int id;
    public int relevation = -1;
    public int numMicrochips;
    public int[] datas;
    public float[] xData;
    public float[] yData;
    public float[] zData;
    public int count = 0;

    void Start()
    {
        manager = FindObjectOfType<Manager>();
        numMicrochips = manager.numMicrochips;
        datas = new int[numMicrochips];
        xData = new float[numMicrochips];
        yData = new float[numMicrochips];
        zData = new float[numMicrochips];

        for (int i = 0; i < numMicrochips; i++)
            datas[i] = -1;
        InvokeRepeating("SearchForNeighbours", 60f, timeSearch);
        Invoke("getData", Random.Range(60f, 240f));
    }

    public void getData()
    {
        relevation = (int)Random.Range(0, 14);
        datas[id] = relevation;
        xData[id] = transform.position.x;
        yData[id] = transform.position.y;
        zData[id] = transform.position.z;
    }

    public void receiveDatas(int[] receivedDatas, float[] receivedX, float[] receivedY, float[] receivedZ)
    {
        drop = Random.Range(0f, 1f);
        if (drop >= 0.02f)
        {
            for (int i = 0; i < numMicrochips; i++)
            {
                if (datas[i] == -1 && count < 99)
                {
                    if (receivedDatas[i] != -1)
                    {
                        datas[i] = receivedDatas[i];
                        xData[i] = receivedX[i];
                        yData[i] = receivedY[i];
                        zData[i] = receivedZ[i];
                        count++;
                    }
                }
            }
        } else
        {
            manager.numDrops++;
        }

    }

    void SearchForNeighbours()
    {
        if (manager.microchips[id].GetComponent<Battery>().readyToCharge == false)
            manager.CheckForNeighbours(id);
    }

    public void Stop()
    {
        CancelInvoke("SearchForNeighbours");
    }
}
