using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manager : MonoBehaviour
{
    public GameObject microchip;
    public GameObject[] microchips;
    public int numMicrochips = 100;
    public int[] numComms;

    public GameObject detection;
    public GameObject test;
    public GameObject[] tests;
    private float[] datasHarvestedX;
    private float[] datasHarvestedY;
    private float[] datasHarvestedZ;

    public GameObject[] microchipsHarvested;
    public int[] datasHarvested;
    public int[] percentage;

    public int numDrops = 0;
    public int numLosses = 0;

    void Start()
    {
        microchips = new GameObject[numMicrochips];
        numComms = new int[numMicrochips];
        microchipsHarvested = new GameObject[numMicrochips];

        datasHarvested = new int[numMicrochips];
        datasHarvestedX = new float[numMicrochips];
        datasHarvestedY = new float[numMicrochips];
        datasHarvestedZ = new float[numMicrochips];

        percentage = new int[numMicrochips];
        tests = new GameObject[numMicrochips];

        for (int i = 0; i < numMicrochips; i++)
        {
            microchips[i] = Instantiate(microchip);
            microchips[i].GetComponent<Communication>().id = i;
            datasHarvested[i] = -1;
            percentage[i] = -1;
        }
    }

    public void CheckForNeighbours(int id)
    {
        for (int i = 0; i < numMicrochips; i++)
        {
            if ((id != i) && (Vector3.Distance(microchips[id].transform.position, microchips[i].transform.position) < 10f)
                && (microchips[id].GetComponent<Battery>().readyToConnect == true
                && microchips[i].GetComponent<Battery>().readyToConnect == true))
            {
                microchips[id].GetComponent<Battery>().readyToConnect = false;
                microchips[i].GetComponent<Battery>().readyToConnect = false;
                StartCoroutine(Communicate(id, i));
                numComms[id] += 1;
                numComms[i] += 1;
            }
        }
    }

    IEnumerator Communicate(int id1, int id2)
    {
        microchips[id1].GetComponent<Battery>().ConsumeBattery();
        microchips[id2].GetComponent<Battery>().ConsumeBattery();

        microchips[id1].GetComponent<Renderer>().material.color = Color.green;
        microchips[id2].GetComponent<Renderer>().material.color = Color.green;

        microchips[id1].GetComponent<Communication>().receiveDatas(microchips[id2].GetComponent<Communication>().datas,
            microchips[id2].GetComponent<Communication>().xData, microchips[id2].GetComponent<Communication>().yData, microchips[id2].GetComponent<Communication>().zData);
        microchips[id2].GetComponent<Communication>().receiveDatas(microchips[id1].GetComponent<Communication>().datas,
            microchips[id2].GetComponent<Communication>().xData, microchips[id2].GetComponent<Communication>().yData, microchips[id2].GetComponent<Communication>().zData);

        yield return new WaitForSecondsRealtime(1f);

        microchips[id1].GetComponent<Battery>().readyToCharge = true;
        microchips[id2].GetComponent<Battery>().readyToCharge = true;

        microchips[id1].GetComponent<Renderer>().material.color = Color.red;
        microchips[id2].GetComponent<Renderer>().material.color = Color.red;
    }

    public void harvesting()
    {
        // Calcolo numero dati raccolti per ciascun microchip
        int count0 = 0;
        int sum0 = 0;
        int min0 = 100;
        int max0 = -1;
        int[] numDatasHarvested = new int[numMicrochips];
        for (int k = 0; k < numMicrochips; k++)
        {
            int[] mydata = microchips[k].GetComponent<Communication>().datas;
            for (int i = 0; i < numMicrochips; i++)
            {
                if (mydata[i] != -1)
                    count0++;
            }
            numDatasHarvested[k] = count0;
            count0 = 0;
        }
        string word = string.Join(", ", numDatasHarvested.Select(i => i.ToString()).ToArray());
        Debug.Log("Dati " + word);
        for (int i = 0; i < numMicrochips; i++)
        {
            sum0 += numDatasHarvested[i];
            if (numDatasHarvested[i] < min0)
                min0 = numDatasHarvested[i];
            if (numDatasHarvested[i] > max0)
                max0 = numDatasHarvested[i];
        }
        int media1 = sum0 / numMicrochips;
        Debug.Log("Media " + media1);
        Debug.Log("Min " + min0);
        Debug.Log("Max " + max0);

        //Calcolo numero connessioni per ciascun microchip
        int sum1 = 0;
        int min1 = 100;
        int max1 = -1;
        string result = string.Join(", ", numComms.Select(i => i.ToString()).ToArray());
        Debug.Log("Comms " + result);
        for (int i = 0; i < numMicrochips; i++)
        {
            sum1 += numComms[i];
            if (numComms[i] < min1)
                min1 = numComms[i];
            if (numComms[i] > max1)
                max1 = numComms[i];
        }
        int media = sum1 / numMicrochips;
        Debug.Log("Media " + media);
        Debug.Log("Min " + min1);
        Debug.Log("Max " + max1);


        //Calcolo numero dati raccolti per ciascun mircochip raccolto dal probe
        int sum2 = 0;
        int min2 = 100;
        int max2 = -1;
        int countDatas = 0;
        int count2 = 0;
        int countDatasHarvested = 0;
        int[] numDatasHarvested2 = new int[numMicrochips];
        for (int i = 0; i < numMicrochips; i++)
        {
            if (microchipsHarvested[i] != null)
            {
                count2++;
                int[] mydata = microchipsHarvested[i].GetComponent<Communication>().datas;
                float[] myX = microchipsHarvested[i].GetComponent<Communication>().xData;
                float[] myY = microchipsHarvested[i].GetComponent<Communication>().yData;
                float[] myZ = microchipsHarvested[i].GetComponent<Communication>().zData;
                for (int k = 0; k < numMicrochips; k++)
                {
                    if (mydata[k] != -1)
                    {
                        countDatas++;
                        if (datasHarvested[k] == -1)
                        {
                            countDatasHarvested++;
                            datasHarvested[k] = mydata[k];
                            datasHarvestedX[k] = myX[k];
                            datasHarvestedY[k] = myY[k];
                            datasHarvestedZ[k] = myZ[k];
                        }
                    }
                }
                numDatasHarvested2[i] = countDatas;
                countDatas = 0;
            }
        }
        string word2 = string.Join(", ", numDatasHarvested2.Select(i => i.ToString()).ToArray());
        Debug.Log(word2);
        Debug.Log("Raccolti " + count2 + " droni");

        for (int i = 0; i < numMicrochips; i++)
        {
            if (numDatasHarvested2[i] != 0)
            {
                sum2 += numDatasHarvested2[i];
                if (numDatasHarvested2[i] < min2)
                    min2 = numDatasHarvested2[i];
                if (numDatasHarvested2[i] > max2)
                    max2 = numDatasHarvested2[i];
            }
        }
        int media2 = sum2 / count2;
        Debug.Log("Media " + media2);
        Debug.Log("Min " + min2);
        Debug.Log("Max " + max2);

        string datas = string.Join(", ", datasHarvested.Select(i => i.ToString()).ToArray());
        Debug.Log(datas);
        Debug.Log("Raccolti " + countDatasHarvested + " dati");
        Debug.Log("Droppati " + numDrops + " pacchetti");
        Debug.Log("Persi " + numLosses + " droni");


        //Calcolo percentuale
        int countP = 0;
        int[] testP = new int[numMicrochips];
        for(int k = 0; k < numMicrochips; k++)
        {
            int[] mydata = microchips[k].GetComponent<Communication>().datas;
            for (int i = 0; i < numMicrochips; i++)
            {
                if (mydata[i] != -1 && percentage[i] != 0)
                {
                    countP++;
                    percentage[i] = 0;
                }
            }
            testP[k] = countP;
        }
        string pp = string.Join(", ", testP.Select(i => i.ToString()).ToArray());
        Debug.Log(pp);

        for (int i = 0; i < numMicrochips; i++)
        {
            if (datasHarvested[i] != -1)
            {
                Vector3 pos = new Vector3(datasHarvestedX[i], datasHarvestedY[i], datasHarvestedZ[i]);
                tests[i] = Instantiate(test, pos, Quaternion.identity);
            }
        }
        
        for(int i = -5; i < 5; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                for (int k = 2; k < 26; k++)
                {
                    Vector3 pos = new Vector3(i * 30, j * 30, k * 50);
                    Instantiate(detection, pos, Quaternion.identity);
                }
            }
        }

        for (int i = 0; i < numMicrochips; i++)
            Destroy(microchips[i]);

    }
}