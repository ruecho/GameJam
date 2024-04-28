using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerSpawn thisSpawn;
    CheckpointSystem chckpntsys;
    GameObject player;
    bool isCheckpoint = false;
    public Transform top;
    public Transform bot;
    public Transform left;
    public Transform right;
    void Start()
    {
        chckpntsys = FindFirstObjectByType<CheckpointSystem>();
        player = FindFirstObjectByType<PlayerController>().gameObject;
    }

    void Update()
    {
        if(!isCheckpoint)
        {
            if(player.transform.position.y < top.position.y &&
            player.transform.position.y > bot.position.y)
            {
                if(player.transform.position.x > left.position.x &&
                player.transform.position.x < right.position.x)
                {
                    chckpntsys.ChangeSpawnPoint(thisSpawn);
                    isCheckpoint = true;
                }
            }
        }
    }
}
