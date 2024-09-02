using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnedPlayer : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab1;
    public GameObject cameracinema;

    private void Start()
    {
        // Vérifiez le nombre d'écrans disponibles
        if (Display.displays.Length > 1)
        {
            ActivateLocalDisplay();
        }
        else
        {
            Debug.LogWarning("Il n'y a pas de display secondaire disponible.");
        }
    }

    void ActivateLocalDisplay()
    {
        // Activation des écrans en fonction du client local
        if (PhotonNetwork.IsMasterClient)
        {
            Display.displays[1].Activate();
            Display.displays[2].SetRenderingResolution(0, 0);
            Debug.Log("Display 1 activé par le MasterClient.");
        }
        else
        {
            Display.displays[2].Activate();
            Display.displays[1].SetRenderingResolution(0, 0);
            Debug.Log("Display 2 activé par un client non-Master.");
        }
    }
}
