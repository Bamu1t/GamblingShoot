using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // Méthode pour quitter la salle
    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel("Loading");
        }
        else
        {
            Debug.LogWarning("Le joueur n'est pas dans une salle.");
        }
    }

    // Méthode callback appelée lorsque le joueur a quitté la salle
    public override void OnLeftRoom()
    {
        Debug.Log("Le joueur a quitté la salle.");
        // Vous pouvez ajouter ici la logique supplémentaire après avoir quitté la salle
        // Par exemple, retourner au menu principal ou charger une autre scène
        // PhotonNetwork.LoadLevel("MainMenu");
    }
}
