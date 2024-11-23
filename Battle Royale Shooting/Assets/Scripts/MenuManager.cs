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
        PhotonNetwork.NickName = "Player" + Random.Range(1, 9999); // Assign a random number-labeled username to the player
        Log("Player Name: " + PhotonNetwork.NickName);  // Display this username in the log area
        PhotonNetwork.AutomaticallySyncScene = true; // Enable automatic scene synchronization between windows
        PhotonNetwork.GameVersion = "1"; // Set the game version
        PhotonNetwork.ConnectUsingSettings(); // Connect to the Photon server using predefined settings
    }
    void Log(string message)
    {
        logText.text += "\n"; // Move the text to the next line
        logText.text += message;  // Add the message
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
    void Update()
    {
        
    }
}
