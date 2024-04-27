using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject droplets;
    public GameObject droplet;

    public void spawnDroplet(Vector3 pos)
    {
        foreach (Transform t in droplets.GetComponentsInChildren<Transform>())
        {
            if((t.position-pos).magnitude < 4f)
            {
                return;
            }
        }
        Debug.Log("drop");
        GameObject drop=Instantiate(droplet, droplets.transform);
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
