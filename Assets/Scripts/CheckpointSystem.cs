using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    PlayerSpawn[] allSpawners;
    PlayerSpawn currentSpawn;
    public AudioSource fanfare;
    void Start()
    {
        allSpawners = FindObjectsOfType<PlayerSpawn>();

        foreach (PlayerSpawn plspwn in allSpawners)
        {
            if(plspwn.imFirst) currentSpawn = plspwn;
        }
    }
    public void ChangeSpawnPoint(PlayerSpawn newSpawn)
    {
        currentSpawn = newSpawn;
        fanfare.Play();
    }
}
