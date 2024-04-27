using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAsDrop : MonoBehaviour
{
    public PlayerSpawner ps;
    public PlayerController pc2;

    void Update()
    {
        if(ps != null)
        {
            var pc = ps.GetComponentInChildren<PlayerController>();
            if (pc != null)
            {
                this.transform.position = pc.transform.position;
            }

        }

        if(pc2 != null)
        {
            this.transform.position = pc2.transform.position;
        }
    }
}
