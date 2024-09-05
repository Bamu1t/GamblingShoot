using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManagerGameLobby : MonoBehaviourPunCallbacks
{
    public Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current scene: " + currentScene.name);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Le joueur a quitté la salle.");
        PhotonNetwork.LoadLevel("MainMenu");
    }


    void FixedUpdate()
    {
        int numberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        if (numberOfPlayers >= 2)
        { 
            PhotonNetwork.LoadLevel("GameMaster");
        }
    }
}
