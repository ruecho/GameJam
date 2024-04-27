using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DropPraticle : MonoBehaviour
{
    public float h_velocity = 0f;
    public float v_velocity = 0f;
    public float g = 0f;
    public bool free_fall = false;
    GameObject obj;
    Vector3 realativePos;
    float time_left=1.0f;
    bool inside = false;
    public LayerMask lm;
    public void StartTravel(Vector2 start,float x_distance,float hight,float gravity)
    {
        transform.position = start;
        float fallTime = 2 * Mathf.Sqrt(2 * hight / gravity);
        h_velocity = x_distance / fallTime;
        v_velocity = gravity * fallTime / 2.0f;
        g = gravity;
        free_fall = true;
    }

    void Update()
    {
        if (free_fall)
        {
            var dt = Time.deltaTime;
            Vector3 p = transform.position;
            p.y += dt * v_velocity - dt * dt * g / 2f;
            p.x += dt * h_velocity;
            transform.position = p;
            v_velocity -= dt * g;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.up, 0.0f,lm);
            if (hit.collider != null)
            {
                inside = true;
                time_left = Random.value * 0.1f ;
                obj = hit.collider.gameObject;
                realativePos = transform.position- obj.transform.position;
            }
            if (inside)
            {
                time_left -= dt;
                if(time_left < 0)
                {
                    free_fall = false;
                }
            }
        }
        else
        {
            if (obj != null)
            {
                transform.position = obj.transform.position + realativePos;
            }

        }

    }
}
