using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // Assurez-vous que nous avons au moins deux affichages disponibles
        if (Display.displays.Length > 1)
        {
            // V�rifiez si le joueur est le Master Client
            if (PhotonNetwork.IsMasterClient)
            {
                // Configurez le Master Client pour utiliser le Display 1
                Display.displays[0].Activate();
            }
            else
            {
                // Configurez les clients pour utiliser le Display 2
                Display.displays[1].Activate();
            }
        }
        else
        {
            Debug.LogError("Pas assez d'affichages disponibles. Assurez-vous d'avoir au moins deux affichages connect�s.");
        }
    }

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
