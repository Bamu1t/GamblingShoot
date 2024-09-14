using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRoom : MonoBehaviourPunCallbacks
{
    public void Leftroom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Loading");
    }
}
