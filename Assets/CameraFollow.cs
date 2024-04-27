using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerSpawner ps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pl=ps.GetComponentInChildren<PlayerController>();
        if (pl!=null)
        {
            this.transform.position=pl.transform.position;
        }
    }
}
