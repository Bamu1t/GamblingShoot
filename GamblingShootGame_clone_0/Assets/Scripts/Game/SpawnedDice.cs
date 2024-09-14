using Photon.Pun;
using UnityEngine;

public class SpawnedDice : MonoBehaviourPunCallbacks
{
    // R�f�rence au prefab du d�, � assigner dans l'inspecteur Unity
    public GameObject Dice;

    private void Start()
    {
        // V�rifiez si PhotonView est attach�
        if (photonView == null)
        {
            Debug.LogError("PhotonView n'est pas attach� � cet objet. Assurez-vous que le script est attach� � un objet avec un PhotonView.");
        }
    }

    public void DiceSpawnedForAllPlayer()
    {
        // V�rifiez si Dice est null avant de continuer
        if (Dice == null)
        {
            Debug.LogError("Dice n'est pas assign� dans l'inspecteur. Assurez-vous de l'assigner avant d'ex�cuter le script.");
            return;
        }

        // V�rification si le photonView est null
        if (photonView == null)
        {
            Debug.LogError("PhotonView n'est pas attach� � cet objet. Assurez-vous que le script est attach� � un objet avec un PhotonView.");
            return;
        }

        // Appel du RPC pour spawner le d� pour tous les joueurs
        photonView.RPC("Dicespawner", RpcTarget.All);
    }

    [PunRPC]
    public void Dicespawner()
    {
        Vector3 spawnPosition;

        // D�terminez la position en fonction de si le joueur est le ma�tre
        if (PhotonNetwork.IsMasterClient)
        {
            spawnPosition = new Vector3(0, -8.35f, 1);
        }
        else
        {
            spawnPosition = new Vector3(0, 8.35f, 1);
        }

        // Instanciation du d�
        PhotonNetwork.Instantiate(Dice.name, spawnPosition, Quaternion.identity);
    }
}
