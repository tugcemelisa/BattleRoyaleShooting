using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> spawns = new List<Transform>();
    [SerializeField] List<Transform> spawnsWalk = new List<Transform>();
    [SerializeField] List<Transform> spawnsTurret = new List<Transform>();

    [SerializeField] public TMP_Text playersText;
    GameObject[] players;
    List<string> activePlayers = new List<string>();
    int checkPlayers = 0;
    int randSpawn;

    private int previousPlayerCount;

    void Start()
    {
        randSpawn = Random.Range(0, spawns.Count);
        PhotonNetwork.Instantiate("Player", spawns[randSpawn].position, spawns[randSpawn].rotation);
        Invoke("SpawnEnemy", 5f);
        previousPlayerCount = PhotonNetwork.PlayerList.Length;
    }
    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length < previousPlayerCount)
        {
            ChangePlayersList();
        }
        previousPlayerCount = PhotonNetwork.PlayerList.Length;
    }
    public void ChangePlayersList()
    {
        photonView.RPC("PlayerList", RpcTarget.All);
    }

    [PunRPC]
    public void PlayerList()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        activePlayers.Clear();
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerController>().dead == false)
            {
                activePlayers.Add(player.GetComponent<PhotonView>().Owner.NickName);
            }
        }

        playersText.text = "Players in game : " + activePlayers.Count.ToString();

        if (activePlayers.Count <= 1 && checkPlayers > 0)
        {
            if (activePlayers.Count > 0) // değişiklik
            {
                PlayerPrefs.SetString("Winner", activePlayers[0]); // değişiklik
            }
            var enemies = GameObject.FindGameObjectsWithTag("enemy"); 
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().ChangeHealth(100);
            }
            Invoke("EndGame", 5f);
        }
        checkPlayers++;
    }
    void EndGame()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
        ChangePlayersList();
    }
    public void SpawnEnemy()
    {
        if (photonView.IsMine)
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