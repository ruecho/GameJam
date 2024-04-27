using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerSpawner ps;
    public float r = 0.5f;
    // Update is called once per frame
    void Update()
    {
        if (ps.GetComponentInChildren<PlayerController>() != null)
        {
            Vector3 d = ps.GetComponentInChildren<PlayerController>().transform.position - transform.position;
            if (d.x * d.x + d.y * d.y < r)
            {
                Debug.Log("yyyyyyyyy");
                ps.GetComponentInChildren<PlayerController>().kill();
            }
        }
    }
}
