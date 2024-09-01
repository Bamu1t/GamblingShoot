using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class SpawnedPlayer : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab1;
    public GameObject cameracinema;

    private void Start()
    {
        Vector2 spawnPos = new Vector2(0, 0);

        // Instancie le joueur et r�cup�re la r�f�rence � la nouvelle instance
        GameObject spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab1.name, spawnPos, Quaternion.identity);

        // Change la couleur du joueur en fonction de s'il est MasterClient ou non
        if (PhotonNetwork.IsMasterClient)
        {
            spawnedPlayer.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            spawnedPlayer.GetComponent<SpriteRenderer>().color = Color.black;
        }

        // Assigne la cam�ra pour suivre le joueur instanci�
        cameracinema.GetComponent<CinemachineVirtualCamera>().Follow = spawnedPlayer.transform;
    }
}