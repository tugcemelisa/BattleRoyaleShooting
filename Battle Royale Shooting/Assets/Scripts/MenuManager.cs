using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text logText;
    [SerializeField] TMP_InputField inputField;
    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1, 9999); 
        Log("Player Name: " + PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true; 
        PhotonNetwork.GameVersion = "1"; 
        PhotonNetwork.ConnectUsingSettings();
    }
    void Log(string message)
    {
        logText.text += "\n";
        logText.text += message;
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 15 });
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to the server");
    }
    public override void OnJoinedRoom()
    {
        Log("Joined the lobby");
        PhotonNetwork.LoadLevel("Lobby");
    }
    public void ChangeName()
    {
        PhotonNetwork.NickName = inputField.text;
        Log("New Player name: " + PhotonNetwork.NickName);
    }
}
