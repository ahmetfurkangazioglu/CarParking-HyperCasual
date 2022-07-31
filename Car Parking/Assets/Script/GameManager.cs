using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using AdManager;

public class GameManager : MonoBehaviour
{

    [Header("Canvas Operaton")]
    [SerializeField] GameObject[] GeneralPanel;
    [SerializeField] GameObject[] RewardButton;
    [SerializeField] TextMeshProUGUI[] GeneralText;
    [SerializeField] GameObject[] CarsImage;
    [SerializeField] Texture CorrectCar;  

    [Header("Platform Operaton")]
    [SerializeField] float[] PlatformRotSpeed;
    GameObject Platforms;

    [Header("General Level Operaton")]
    public ParticleSystem Explosion;
    public AudioSource[] Voices;
    [SerializeField] GameObject[] Cars;
    [SerializeField] int HowManyCars;
    [HideInInspector] public bool GetCarLocked;
    [HideInInspector] public int Diamond;
    int LoseCarParked = 0;
    int CarIndex = 0;
    bool GameOver;
    Vector3 NewPos;

    [Header("Optional Level Operaton")]
    [SerializeField] GameObject Circle;
    public bool IsThereRisingPlatform;

    AdMobManager ad = new AdMobManager();
    void Start()
    {
        GetCarLocked = true;
        AdMobSettings();
        SetCarImage();
        NewPos = new Vector3(Cars[0].transform.position.x, Cars[0].transform.position.y, Cars[0].transform.position.z);
        GeneralText[1].text = SceneManager.GetActiveScene().name;
        GeneralText[0].text = PlayerPrefs.GetInt("Diamond").ToString();
        Platforms = GameObject.FindGameObjectWithTag("Platform");
    }

    void Update()
    {

        if (Input.touchCount==1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase==TouchPhase.Began)
            {            
                if(!GetCarLocked)
                {
                    Cars[CarIndex].GetComponent<Car>().IsCarReady = true;
                    CarIndex++;
                    GetCarLocked = true;
                }
            }
        }

        if (!GameOver)
        {
            Platforms.transform.Rotate(new Vector3(0, 0, PlatformRotSpeed[0]), Space.Self);
            if (Circle!=null)
            {
                Circle.transform.Rotate(new Vector3(0, 0, -PlatformRotSpeed[1]), Space.Self);
            }
        }
       
    }


    public void Lose()
    {
        ad.ShowInterstitial();
        Voices[1].Play();
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
        GetCarLocked = false;
        CarsImage[CarIndex - 1].GetComponent<RawImage>().texture = CorrectCar;
        if (CarIndex<HowManyCars)
        {
            Cars[CarIndex].SetActive(true);
            LoseCarParked++;
        }
        else
        {
            GetCarLocked = true;
            ad.ShowInterstitial();
            Voices[3].Play();
            Voices[2].Play();
            GeneralPanel[2].SetActive(true);
            PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + Diamond);
            GeneralText[2].text = PlayerPrefs.GetInt("Diamond").ToString();
            GeneralText[3].text = SceneManager.GetActiveScene().name;
            GeneralText[4].text = Diamond.ToString();
            GeneralText[5].text = HowManyCars.ToString();
            Invoke("WinTextDisable", 2f);
        }
    }

    public void LoadScene(string value)
    {
        switch (value)
        {
            case "Win":
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "Lose":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Settings":
                GeneralPanel[6].SetActive(true);
                break;
            case "CloseDefault":
                GeneralPanel[1].SetActive(false);
                GeneralPanel[0].SetActive(true);
                GetCarLocked = false;
                break;
        }
    }
    public void RewardsOperation(string Value)
    {
        switch (Value)
        {
            case "DisableDiamond":
                RewardButton[0].SetActive(false);
                break;
            case "DisableRevive":
                RewardButton[1].SetActive(false);
                break;
            case "ShowDiamond":
                ad.ShowReward();
                break;
            case "ShowRevive":
                ad.ShowReviveReward();
                break;
        }
    }
    public void DiamondRewars(double RewardAmount)
    {
        int Reward = (int)RewardAmount;
        PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + ((Diamond * Reward) - Diamond));
        GeneralText[2].text = PlayerPrefs.GetInt("Diamond").ToString();
    }
    public void ReviveRewars()
    {
        GeneralPanel[3].SetActive(false);
        CarIndex--;
        Cars[CarIndex].SetActive(false);
        Cars[CarIndex].transform.position = NewPos;
        Cars[CarIndex].SetActive(true);
        Cars[CarIndex].GetComponent<Car>().StopPoint = false;
        GameOver = false;
    }
    void SetCarImage()
    {
        for (int i = 0; i < HowManyCars; i++)
        {
            CarsImage[i].SetActive(true);
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
    void AdMobSettings()
    {
        ad.RequestInterstitialAd();
        ad.RequestReviveRewardAd();
        ad.RequestRewardAd();
    }
}
