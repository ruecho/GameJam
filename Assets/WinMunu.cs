using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMunu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popup;
    public TMPro.TMP_Text te;
    public void showScore(int numberOfDeaths)
    {
        Debug.Log("win after"+ numberOfDeaths.ToString());
        popup.SetActive(true);
        te.text = te.text.Replace("XXX", numberOfDeaths.ToString());
    }
}
