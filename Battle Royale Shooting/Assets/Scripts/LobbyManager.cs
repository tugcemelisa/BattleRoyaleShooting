using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text ChatText;
    [SerializeField] TMP_InputField InputText;
    [SerializeField] TMP_Text PlayersText;
    void Start()
    {
        RefreshPlayers();
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Log(newPlayer.NickName + " entered the room");
        RefreshPlayers();
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Log(otherPlayer.NickName + " left the room");
        RefreshPlayers();
    }
    void Update()
    {
        
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    void Log(string message)
    {
        ChatText.text += "\n";
        ChatText.text += message;
    }
   
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    
    [PunRPC]
    public void ShowMessage(string message, PhotonMessageInfo info)
    {
        ChatText.text += "\n";
        ChatText.text += message;
    }
    public void Send()
    {
        if (string.IsNullOrWhiteSpace(InputText.text)) { return; }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            photonView.RPC("ShowMessage", RpcTarget.All, PhotonNetwork.NickName + ": " + InputText.text);
            InputText.text = string.Empty;
        }
    }
    void RefreshPlayers()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("ShowPlayers", RpcTarget.All);
        }
    }

    [PunRPC]
    public void ShowPlayers()
    {
        PlayersText.text = "Players: ";
        foreach (Photon.Realtime.Player otherPlayer in PhotonNetwork.PlayerList)
        {
            PlayersText.text += "\n";
            PlayersText.text += otherPlayer.NickName;
        }
    }
}
