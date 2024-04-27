using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public GameObject player;
    public float distanceToPlayer;
    public float triggerMovementAtMin;
    public float triggerMovementAtMax;
    public float maxCamY;
    public float minCamY;
    void Start()
    {
        
    }
    void Update()
    {        
            Vector3 newCameraPosition = new Vector3(transform.position.x, player.transform.position.y + distanceToPlayer, transform.position.z);
            Vector3 translation = Vector3.Lerp(transform.position, newCameraPosition, Time.deltaTime);
            float clampedY = Mathf.Clamp(translation.y, minCamY, maxCamY);
            translation.y = clampedY;
            transform.position = translation;        
    }
}
