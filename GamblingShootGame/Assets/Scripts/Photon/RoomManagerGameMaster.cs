using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerGameMaster : MonoBehaviourPunCallbacks
{
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master Client : Configuration pour la camera GameMaster");
        }
        else
        {
            Debug.Log("Client : Configuration pour la camera GameClient");
            Camera.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
