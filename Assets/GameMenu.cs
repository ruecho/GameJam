using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject CameraRig;
    public UnityEngine.UI.Button StartButton;
    public UnityEngine.UI.Button ExitButton;
    public UnityEngine.UI.Image mask;
    public AudioSource startSoundEffect;
    public AudioSource endSoundEffect;

    int numberOfDeaths = 0;
    bool isLanching = false;
    bool isLanchingv2 = false;
    float timeToLunch = 2.0f;
    bool isEnding = false;
    float timeToEnd = 2.0f;

    public WinMunu winMunu;

    public void AddDeath()
    {
        numberOfDeaths++;
    }


    void Start()
    {
        Menu.gameObject.SetActive(true);

        StartButton.onClick.AddListener(() =>{

            StartGame();
        });

        ExitButton.onClick.AddListener(() =>
        {
            Debug.Log("Exit");
            Application.Quit();
        });

    }

    void StartGame()
    {
        StartButton.gameObject.SetActive(false);
        startSoundEffect.Play();
        isLanching = true;
        Debug.Log("SG");
        timeToLunch = 2.0f;
    }
    void Update()
    {
        if (isLanching)
        {
            //Debug.Log("TICK ST");
            Color c = mask.color;
            c.a = 1f - timeToLunch / 2.0f;
            mask.color = c;
            timeToLunch -= Time.deltaTime;
            if (timeToLunch < 0.0f)
            {
                isLanching = false;
                Menu.gameObject.SetActive(false);
                CameraRig.gameObject.SetActive(true);
                isLanchingv2 = true;
                timeToLunch = 2f;
            }
        }
        if (isLanchingv2)
        {
            Color c = mask.color;
            c.a =  timeToLunch / 2.0f;
            mask.color = c;
            timeToLunch -= Time.deltaTime;
            if(timeToLunch < 0.0f)
            {
                c = mask.color;
                c.a = 0.0f;
                mask.color = c;
                EnablePlayer();
                isLanchingv2 = false;
            }
        }
        if (isEnding)
        {
            Debug.Log("Ending..");
            Color c = mask.color;
            c.a = 1f - timeToLunch / 2.0f;
            mask.color = c;
            timeToLunch -= Time.deltaTime;
            if (timeToLunch < 0.0f)
            {
                isEnding = false;
                winMunu.showScore(numberOfDeaths);
            }
        }
    }
    void EnablePlayer()
    {
        FindFirstObjectByType<PlayerController>().isEnabled = true;
    }
    public void StopGame()
    {
        endSoundEffect.Play();
        isEnding = true;
        timeToEnd = 2f;
    }
}
