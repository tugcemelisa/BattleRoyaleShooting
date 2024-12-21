using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> spawns = new List<Transform>();
    [SerializeField] List<Transform> spawnsWalk = new List<Transform>();
    [SerializeField] List<Transform> spawnsTurret = new List<Transform>();
    int randSpawn;
    void Start()
    {
        randSpawn = Random.Range(0, spawns.Count);
        PhotonNetwork.Instantiate("Player", spawns[randSpawn].position, spawns[randSpawn].rotation);
        Invoke("SpawnEnemy", 5f);
    }
    public void SpawnEnemy()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < spawnsWalk.Count; i++)
            {
                PhotonNetwork.Instantiate("WalkEnemy", spawnsWalk[i].position, spawnsWalk[i].rotation);
            }
            for (int i = 0; i < spawnsTurret.Count; i++)
            {
                PhotonNetwork.Instantiate("Turret", spawnsTurret[i].position, spawnsTurret[i].rotation);
            }
        }
    }
}