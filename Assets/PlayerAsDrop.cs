using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAsDrop : MonoBehaviour
{
    public PlayerSpawner ps;

    void Update()
    {
        var pc = ps.GetComponentInChildren<PlayerController>();
        if(pc != null)
        {
            this.transform.position = pc.transform.position;
        }
    }
}
