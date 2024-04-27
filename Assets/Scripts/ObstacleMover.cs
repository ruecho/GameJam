using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleMover : MonoBehaviour
{
    public GameObject obstacle;
    public float speed;    
    public Transform[] allDestinations;
    public float[] destinationTimers;

    int index = 0;
    float nextStepTimer;
    
    void Update()
    {
        if(nextStepTimer <= 0) 
        {
            index++;
            if (index >= allDestinations.Length)
                index = 0;

            nextStepTimer = destinationTimers[index];
        }
        Vector3 direction = obstacle.transform.position - allDestinations[index].position;
        direction.Normalize();
        
        if (Vector3.Distance(obstacle.transform.position + (direction * speed * Time.deltaTime), allDestinations[index].position) > 0.1f)
        {
            nextStepTimer -= Time.deltaTime;
        }
        else
            obstacle.transform.Translate(direction * speed * Time.deltaTime);
    }
}
