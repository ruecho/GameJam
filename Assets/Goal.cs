using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public PlayerSpawner ps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.GetComponentInChildren<PlayerController>() != null)
        {
            Vector3 d = ps.GetComponentInChildren<PlayerController>().transform.position - transform.position;
            if (d.x*d.x+d.y*d.y<3.0){
                Debug.Log("XXXXXXXXXXx");
                SceneManager.LoadScene("WIN");
            }
        }
    }
}
