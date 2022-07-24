using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    bool StopPoint;
    public bool IsCarReady;
    public GameManager manager;
    public GameObject[] WheelTruck;
    Transform Parent;
    void Start()
    {       
        Parent = GameObject.FindGameObjectWithTag("Platform").transform;
        
    }

    void Update()
    {
        if (IsCarReady)
        {
            transform.Translate(transform.forward * 15f * Time.deltaTime);
        }
        if (!StopPoint)
        {
            transform.Translate(transform.forward * 7f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FirstStop"))
        {
            StopPoint = true;
            manager.FirstStop.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Stop"))
        {          
            IsCarReady = false;
            WheelTruck[0].SetActive(false);
            WheelTruck[1].SetActive(false);
            manager.FirstStop.SetActive(true);
            transform.SetParent(Parent);
            manager.SetCar();
        }
        else if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.SetActive(false);
        }
    }
}
