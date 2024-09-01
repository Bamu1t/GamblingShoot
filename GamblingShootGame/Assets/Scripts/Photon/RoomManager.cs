using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // M�thode pour quitter la salle
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

    // M�thode callback appel�e lorsque le joueur a quitt� la salle
    public override void OnLeftRoom()
    {
        Debug.Log("Le joueur a quitt� la salle.");
        // Vous pouvez ajouter ici la logique suppl�mentaire apr�s avoir quitt� la salle
        // Par exemple, retourner au menu principal ou charger une autre sc�ne
        // PhotonNetwork.LoadLevel("MainMenu");
    }
}
