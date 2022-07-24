using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int CarIndex = 0;
    public int HowManyCars;
    public GameObject[] Cars;
    public float[] PlatformRotSpeed;
    private GameObject Platforms;
    public GameObject FirstStop;
    void Start()
    {
        Platforms = GameObject.FindGameObjectWithTag("Platform");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cars[CarIndex].GetComponent<Car>().IsCarReady = true;
            CarIndex++;
        }
        Platforms.transform.Rotate(new Vector3(0,0, PlatformRotSpeed[0]), Space.Self);
    }


    public void SetCar()
    {
        if (CarIndex<HowManyCars)
        {
            Cars[CarIndex].SetActive(true);
        }
    }
}
