using Photon.Pun;
using UnityEngine;

public class SpawnedDice : MonoBehaviourPunCallbacks
{
    // Référence au prefab du dé, à assigner dans l'inspecteur Unity
    public GameObject Dice;

    private void Start()
    {
        // Vérifiez si PhotonView est attaché
        if (photonView == null)
        {
            Debug.LogError("PhotonView n'est pas attaché à cet objet. Assurez-vous que le script est attaché à un objet avec un PhotonView.");
        }
    }

    public void DiceSpawnedForAllPlayer()
    {
        // Vérifiez si Dice est null avant de continuer
        if (Dice == null)
        {
            Debug.LogError("Dice n'est pas assigné dans l'inspecteur. Assurez-vous de l'assigner avant d'exécuter le script.");
            return;
        }

        // Vérification si le photonView est null
        if (photonView == null)
        {
            Debug.LogError("PhotonView n'est pas attaché à cet objet. Assurez-vous que le script est attaché à un objet avec un PhotonView.");
            return;
        }

        // Appel du RPC pour spawner le dé pour tous les joueurs
        photonView.RPC("Dicespawner", RpcTarget.All);
    }

    [PunRPC]
    public void Dicespawner()
    {
        Vector3 spawnPosition;

        // Déterminez la position en fonction de si le joueur est le maître
        if (PhotonNetwork.IsMasterClient)
        {
            spawnPosition = new Vector3(0, -8.35f, 1);
        }
        else
        {
            spawnPosition = new Vector3(0, 8.35f, 1);
        }

        // Instanciation du dé
        PhotonNetwork.Instantiate(Dice.name, spawnPosition, Quaternion.identity);
    }
}
