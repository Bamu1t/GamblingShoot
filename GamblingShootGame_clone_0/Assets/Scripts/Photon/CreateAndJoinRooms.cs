using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField joinInput;
    public Toggle privateroom;
    public byte maxPlayersPerRoom = 2;
    public string lobbySceneName = "Lobby"; // Nom de la sc�ne initiale
    public string masterSceneName = "GameMaster"; // Nom de la sc�ne pour le Master Client
    public string clientSceneName = "GameClient"; // Nom de la sc�ne pour les Clients

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = !privateroom.isOn; // Salle publique si le Toggle n'est pas activ�, priv�e sinon
        roomOptions.MaxPlayers = maxPlayersPerRoom; // Utiliser la variable d�finie pour le nombre max de joueurs

        PhotonNetwork.CreateRoom(GenerateRandomRoomName(), roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameLobby");
    }
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("�chec de la connexion � la salle : " + message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("�chec de la cr�ation de la salle : " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError($"Failed to join random room: {message}");
    }

    private string GenerateRandomRoomName()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int length = 8; // Longueur du nom de salle

        char[] stringChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[Random.Range(0, chars.Length)];
        }

        return new string(stringChars);
    }
}
