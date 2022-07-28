using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Canvas Operaton")]
    [SerializeField] GameObject[] GeneralPanel;
    [SerializeField] TextMeshProUGUI[] GeneralText;
    [SerializeField] GameObject[] CarsImage;
    [SerializeField] Texture CorrectCar;  

    [Header("Platform Operaton")]
    [SerializeField] float[] PlatformRotSpeed;
    GameObject Platforms;

    [Header("Level Operaton")]
    public ParticleSystem Explosion;
    [SerializeField] GameObject[] Cars;
    [SerializeField] int HowManyCars;
    [HideInInspector] public int Diamond;
    int LoseCarParked = 0;
    int CarIndex = 0;
    bool GameOver;
    void Start()
    {
        PlayerPrefControl();
        SetCarImage();
        GeneralText[1].text = SceneManager.GetActiveScene().name;
        GeneralText[0].text = PlayerPrefs.GetInt("Diamond").ToString();
        Platforms = GameObject.FindGameObjectWithTag("Platform");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cars[CarIndex].GetComponent<Car>().IsCarReady = true;
            CarIndex++;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GeneralPanel[1].SetActive(false);
            GeneralPanel[0].SetActive(true);
        }
        if (!GameOver)
        {
            Platforms.transform.Rotate(new Vector3(0, 0, PlatformRotSpeed[0]), Space.Self);
        }
       
    }


    public void Lose()
    {
        GeneralPanel[3].SetActive(true);
        GameOver = true;
        GeneralText[6].text= PlayerPrefs.GetInt("Diamond").ToString();
        GeneralText[7].text = SceneManager.GetActiveScene().name;
        GeneralText[8].text = Diamond.ToString();
        GeneralText[9].text = LoseCarParked.ToString();
        Invoke("LoseTextDisable", 2f);
    }
    public void SetCar()
    {
        CarsImage[CarIndex - 1].GetComponent<RawImage>().texture = CorrectCar;
        if (CarIndex<HowManyCars)
        {
            Cars[CarIndex].SetActive(true);
            LoseCarParked++;
        }
        else
        {
            GeneralPanel[2].SetActive(true);
            PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + Diamond);
            GeneralText[2].text = PlayerPrefs.GetInt("Diamond").ToString();
            GeneralText[3].text = SceneManager.GetActiveScene().name;
            GeneralText[4].text = Diamond.ToString();
            GeneralText[5].text = HowManyCars.ToString();
            Invoke("WinTextDisable", 2f);
        }
    }
    void SetCarImage()
    {
        for (int i = 0; i < HowManyCars; i++)
        {
            CarsImage[i].SetActive(true);
        }
    }
    void PlayerPrefControl()
    {
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 1);
            PlayerPrefs.SetInt("Level", 1);
        }
    }
    void WinTextDisable()
    {
        GeneralPanel[4].SetActive(true);
    }
    void LoseTextDisable()
    {
        GeneralPanel[5].SetActive(true);
    }
   public void LoadScene(string value)
    {
        switch (value)
        {
            case "Win":
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                break;
            case "Lose":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }
}
