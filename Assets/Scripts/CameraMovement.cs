using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offsetU = new Vector3(0, 2f, 0);
    private Vector3 offsetD = new Vector3(0, -2f, 0);

    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public GameObject probe;
    private int velocity = 1;

    public float speed = 12f;
    public CharacterController controller;
    private bool justOnce = true;

    void FixedUpdate()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = transform.position + offsetU;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position = transform.position + offsetD;
        }

        if (Input.GetKeyDown("m"))
        {
            Time.timeScale += velocity;
            Debug.Log(Time.timeScale);
        }

        if (Input.GetKeyDown("n"))
        {
            Time.timeScale = 1;
            Debug.Log(Time.timeScale);
        }

        if (Input.GetKeyDown("p"))
        {
            Time.timeScale = 0;
            Debug.Log(Time.timeScale);
        }

        if (Input.GetKeyDown("h") && justOnce == true)
        {
            FindObjectOfType<Manager>().harvesting();
            justOnce = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Instantiate(probe, hit.point, Quaternion.identity);
            }
        }
    }
}
