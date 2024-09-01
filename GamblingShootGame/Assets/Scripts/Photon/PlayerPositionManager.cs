using UnityEngine;
using Photon.Pun;

public class PlayerPositionManager : MonoBehaviourPunCallbacks
{
    private GameObject firstPlayer;
    private GameObject secondPlayer;

    void Start()
    {
        // Trouver tous les objets avec le tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length >= 2)
        {
            // Assigner les joueurs selon leur ordre dans la liste
            firstPlayer = players[0];
            secondPlayer = players[1];
        }
        else
        {
            Debug.LogWarning("Il n'y a pas assez de joueurs dans la scène.");
        }
    }

    void Update()
    {
        if (firstPlayer != null && secondPlayer != null)
        {
            // Obtenir les positions des deux joueurs
            Vector3 firstPlayerPosition = firstPlayer.transform.position;
            Vector3 secondPlayerPosition = secondPlayer.transform.position;

            // Afficher les positions dans la console
            Debug.Log("Position du premier joueur : " + firstPlayerPosition);
            Debug.Log("Position du deuxième joueur : " + secondPlayerPosition);

            // Exemple : Calculer la distance entre les deux joueurs
            float distance = Vector3.Distance(firstPlayerPosition, secondPlayerPosition);
            Debug.Log("Distance entre les deux joueurs : " + distance);
        }
    }
}
