using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject droplets;
    public GameObject droplet;

    public void spawnDroplet(Vector3 pos,Vector3 velocity,bool check=true)
    {
        foreach (Transform t in droplets.GetComponentsInChildren<Transform>())
        {
            if((t.position-pos).magnitude < 0.5f)
            {
                if (check)
                {
                    return;
                }
            }
        }
        Debug.Log("drop");
        GameObject drop=Instantiate(droplet, droplets.transform);
        drop.GetComponent<Rigidbody2D>().velocity = velocity;
        drop.transform.position = pos;
    }


    void Update()
    {
        if (this.GetComponentInChildren<PlayerController>()==null)
        {
            Debug.Log("NO PLAYER");
            Instantiate(prefab,transform);
        }
    }
}
