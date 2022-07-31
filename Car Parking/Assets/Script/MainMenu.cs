using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   [SerializeField] AudioSource GameSound;
    [SerializeField] GameObject SurePanel;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 1);
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetFloat("MenuSound", 0.3f);
            PlayerPrefs.SetInt("CurrentAd", 0);
        }
        GameSound.volume = PlayerPrefs.GetFloat("MenuSound");
    }

  public  void LoadScene(string Value)
    {
        switch (Value)
        {
            case "1":
                SceneManager.LoadScene(1);
                break;
            case "2":
                SceneManager.LoadScene(2);
                break;
            case "3":
                SceneManager.LoadScene(3);
                break;
            case "4":
                SceneManager.LoadScene(4);
                break;
            case "Sure":
                SurePanel.SetActive(true);
                break;
            case "Yes":
                Application.Quit();
                break;
            case "No":
                SurePanel.SetActive(false);
                break;
            case "Default":
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
                break;
        }
    }
}
