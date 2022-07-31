using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [HideInInspector] public bool IsCarReady;
    [SerializeField] GameManager manager;
    [SerializeField] GameObject[] WheelTruck;
    [SerializeField] GameObject ExplosionPoint;
    [HideInInspector] public bool StopPoint;
    Transform Platform;
    bool StopControl;
    bool StartRising;
    double RisingValue;
    bool CheckCarStop;
    void Start()
    {
        Platform = GameObject.FindGameObjectWithTag("Platform").transform;
        
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
        if (StartRising)
        {
            if (RisingValue>Platform.position.y)
            {
                Platform.position = Vector3.Lerp(Platform.position, new Vector3(Platform.position.x, Platform.position.y + 1.3f, Platform.position.z), .0080f);
            }
            else
            {
                StartRising = false;
            }
           
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Car"))
        {
            CarSettings();
            manager.GetCarLocked = true;
            manager.Explosion.transform.position = ExplosionPoint.transform.position;
            manager.Explosion.Play();
            manager.Lose();        
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstStop"))
        {
            StopPoint = true;
            if (CheckCarStop)
            {
                manager.GetCarLocked = false;
            }
            CheckCarStop = true;
        }
       else  if (other.gameObject.CompareTag("Stop"))
        {
            if (StopControl)
            {
                StopControl = false;
                CarSettings();
                transform.SetParent(Platform);
                manager.SetCar();
                RisePlatformOperation();
            }    
        }
        else if (other.gameObject.CompareTag("FirstControl"))
        {       
            StopControl = true;
        }
        else if (other.gameObject.CompareTag("Platform"))
        {        
            manager.Explosion.transform.position = ExplosionPoint.transform.position;
            manager.Explosion.Play();
            manager.Lose();
            CarSettings();
            manager.GetCarLocked = true;
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            manager.Voices[0].Play();
            manager.Diamond++;
            other.gameObject.SetActive(false);
        }
      
    }
    void RisePlatformOperation()
    {
        if (manager.IsThereRisingPlatform)
        {
            StartRising = true;
           RisingValue= Platform.position.y + 1.3;
        }
    }
    void CarSettings()
    {
        IsCarReady = false;
        WheelTruck[0].SetActive(false);
        WheelTruck[1].SetActive(false);
    }
}
