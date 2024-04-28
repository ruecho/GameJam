using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public Canvas Menu;
    public GameObject CameraRig;
    public UnityEngine.UI.Button StartButton;
    public UnityEngine.UI.Button ExitButton;
    void Start()
    {
        Menu.gameObject.SetActive(true);

        StartButton.onClick.AddListener(() =>{
            Menu.gameObject.SetActive(false);
            CameraRig.gameObject.SetActive(true);
        });

        ExitButton.onClick.AddListener(() =>
        {
            Debug.Log("Exit");
            Application.Quit();
        });

    }

}
