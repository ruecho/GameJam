using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject droplets;
    public GameObject droplet;

    public bool spawnDropletCheck(Vector3 pos, Vector3 velocity)
    {
        foreach (Transform t in droplets.GetComponentsInChildren<Transform>())
        {
            if ((t.position - pos).magnitude < 0.5f)
            {
                    return false;

            }
        }
        return true;
    }

    public void spawnDropletBoom(Vector3 pos, float dropletsSpeed, int num)
    {
        /*
            for (int i = 0; i < num; i++)
            {
                float a = i*Mathf.PI * 2.0f / num;
                this.GetComponentInParent<PlayerSpawner>().spawnDroplet(pos, new Vector2(Mathf.Sin(a), Mathf.Cos(a)) * dropletsSpeed, false);
            }
        */
        foreach (Transform t in droplets.GetComponentsInChildren<Transform>())
        {
            if ((t.position - pos).magnitude < 0.5f)
            {
                if(t.GetComponent<DropPraticle>()!=null)
                    return;
            }
        }

        for (int i = 0; i < num; i++)
        {
            GameObject drop = Instantiate(droplet, droplets.transform);
            float delta_x = (i /(float) (num - 1))*2f-1f;
            drop.GetComponent<DropPraticle>().StartTravel(pos,delta_x*1f,2f+ Mathf.Abs(delta_x) * 0.1f, 9f);
        }
    }

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
        drop.transform.position = pos;
        DropPraticle drp = drop.GetComponent<DropPraticle>();
        
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
