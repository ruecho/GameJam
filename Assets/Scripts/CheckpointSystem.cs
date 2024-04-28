using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{    
    public PlayerSpawn currentSpawn;
    public AudioSource fanfare;
    
    public void ChangeSpawnPoint(PlayerSpawn newSpawn)
    {
        currentSpawn = newSpawn;
        fanfare.Play();
    }
}
