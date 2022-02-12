using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private float life;
    private float lifeTime;

    public Manager manager;

    void Start()
    {
        life = Random.Range(0f, 1f);
        if (life < 0.05f)
        {
            manager = FindObjectOfType<Manager>();
            lifeTime = Random.Range(10f, 180f);
            InvokeRepeating("decreaseLife", 1f, 1f);
        }
    }

    public void decreaseLife()
    {
        lifeTime -= 1f;
        if (lifeTime < 0)
        {
            manager.numLosses++;
            gameObject.SetActive(false);
            StopLife();
        }
    }

    public void StopLife()
    {
        CancelInvoke("decreaseLife");
    }
}
