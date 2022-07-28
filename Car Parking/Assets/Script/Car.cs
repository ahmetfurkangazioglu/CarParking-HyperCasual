using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [HideInInspector] public bool IsCarReady;
    [SerializeField] GameManager manager;
    [SerializeField] GameObject[] WheelTruck;
    [SerializeField] GameObject ExplosionPoint;
    bool StopPoint;
    Transform Parent;
    bool StopControl;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstStop"))
        {
            StopPoint = true;
        }
       else  if (other.gameObject.CompareTag("Stop"))
        {
            if (StopControl)
            {
                StopControl = false;
                IsCarReady = false;
                WheelTruck[0].SetActive(false);
                WheelTruck[1].SetActive(false);
                transform.SetParent(Parent);
                manager.SetCar();
            }    
        }
        else if (other.gameObject.CompareTag("FirstControl"))
        {       
            StopControl = true;
        }
        else if (other.gameObject.CompareTag("Platform"))
        {
            gameObject.SetActive(false);
            manager.Explosion.transform.position = ExplosionPoint.transform.position;
            manager.Explosion.Play();
            manager.Lose();
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            manager.Diamond++;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Car"))
        {
            IsCarReady = false;
            manager.Explosion.transform.position = ExplosionPoint.transform.position;
            manager.Explosion.Play();
            manager.Lose();
        }
    }
}
