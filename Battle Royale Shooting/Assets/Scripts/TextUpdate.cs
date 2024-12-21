using UnityEngine;
using TMPro;
using Photon.Pun;

public class TextUpdate : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] TMP_Text playerNickName;
    int health = 100;
    void Start()
    {
        if (photonView.IsMine)
        {
            playerNickName.text = photonView.Controller.NickName + "\n" + "Health: " + health.ToString();
            photonView.RPC("RotateName", RpcTarget.Others);
        }
    }
    public void SetHealth(int newHealth)
    {
        health = newHealth;
        playerNickName.text = photonView.Controller.NickName + "\n" + "Health: " + health.ToString();
    }

    [PunRPC]
    public void RotateName()
    {
        playerNickName.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
            playerNickName.text = photonView.Controller.NickName + "\n" + "Health: " + health.ToString();
        }
    }
}