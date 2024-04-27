using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMaker : MonoBehaviour
{
    public GameObject droplets;
    public GameObject droplet;
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
                if (t.GetComponent<DropPraticle>() != null)
                    return;
            }
        }

        for (int i = 0; i < num; i++)
        {
            GameObject drop = Instantiate(droplet, droplets.transform);
            float delta_x = (i / (float)(num - 1)) * 2f - 1f;
            drop.GetComponent<DropPraticle>().StartTravel(pos, delta_x * 1f, 2f + Mathf.Abs(delta_x) * 0.1f, 9f);
        }
    }
}
