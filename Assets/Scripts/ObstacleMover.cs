using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleMover : MonoBehaviour
{
    public GameObject obstacle;
    public float timeToArrive;    
    public Transform[] allDestinations;
    public float[] destinationTimers;

    int index = 0;
    float nextStepTimer;
    bool resting = true;
    float progress = 0;
    Vector3 lastPosition;
    
    void Start()
    {
        nextStepTimer = destinationTimers[index];
    }
    void Update()
    {
        if (resting)
        {
            nextStepTimer -= Time.deltaTime;
            if (nextStepTimer <= 0)
            {
                index++;
                if (index >= allDestinations.Length)
                {
                    index = 0;
                }
                resting = false;
                progress = 0;
                lastPosition = obstacle.transform.position;
            }
        }
        if(!resting)
        {
            progress += Time.deltaTime;
            if(progress >= timeToArrive) progress = timeToArrive;
            Vector3 newPosition = Vector3.Lerp(lastPosition, allDestinations[index].position, progress/timeToArrive);
            obstacle.transform.position = newPosition;
            if(progress/timeToArrive == 1) resting = true;
        }
    }
}
