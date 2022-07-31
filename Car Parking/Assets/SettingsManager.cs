using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Slider slider;
    public AudioSource GameSound;
    public GameObject SettingPane;
    void Start()
    {
        SetVolume();
    }
    public void SettingManager()
    {
       PlayerPrefs.SetFloat("MenuSound", slider.value);
       GameSound.volume = slider.value;
    }
    public void GeneralOperation(string Value)
    {
        switch (Value)
        {
            case "BackMenu":
                SceneManager.LoadScene(0);
                break;
            case "Continue":
                SettingPane.SetActive(false);
                break;
        }
    }
    void SetVolume()
    {
        slider.value = PlayerPrefs.GetFloat("MenuSound");
        GameSound.volume = slider.value;
    }
}
