using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

/*
 * GridManager Script:
 * 
 * This script is responsible for managing two grids, each containing 9 buttons. Player 1 controls Grid 1 and Player 2 controls Grid 2. 
 * Each player can only interact with the grid they are assigned to, and their actions (button clicks) are synchronized across both players
 * using Photon PUN 2 for multiplayer support.
 * 
 * How it works:
 * 1. We check if the current player is the MasterClient (Player 1) or the other player (Player 2).
 * 2. Depending on which player is connected, we enable or disable their grid’s buttons.
 * 3. When a player clicks a button on their grid, the button becomes unclickable for both players, and a counter specific to that grid 
 *    is incremented.
 * 4. This is done using Photon RPC calls to synchronize button states and counters across the network.
 */

public class GridManager : MonoBehaviourPunCallbacks
{
    // Arrays to hold the buttons for Grid 1 and Grid 2
    public GameObject[] grid1Buttons; // Buttons in Grid 1 (for Player 1)
    public GameObject[] grid2Buttons; // Buttons in Grid 2 (for Player 2)
    
    // UI elements to display the counters for each grid
    public TextMeshProUGUI counter1Text; // UI text for Grid 1 counter
    public TextMeshProUGUI counter2Text; // UI text for Grid 2 counter
    
    // Variables to store the current value of the counters
    private int counter1 = 0; // Counter for Grid 1
    private int counter2 = 0; // Counter for Grid 2

    // Start is called before the first frame update
    void Start()
    {
        // Check if the current player is Player 1 (MasterClient)
        if (PhotonNetwork.IsMasterClient) // Player 1 controls Grid 1
        {
            // Enable buttons in Grid 1 for Player 1 and disable Grid 2 buttons
            EnableGridButtons(grid1Buttons, true);
            EnableGridButtons(grid2Buttons, false);
        }
        else // Player 2 controls Grid 2
        {
            // Enable buttons in Grid 2 for Player 2 and disable Grid 1 buttons
            EnableGridButtons(grid1Buttons, false);
            EnableGridButtons(grid2Buttons, true);
        }
    }

    // Function to enable or disable all buttons in a grid
    void EnableGridButtons(GameObject[] gridButtons, bool enable)
    {
        foreach (GameObject button in gridButtons)
        {
            // Enable or disable the interactable property of the button
            button.GetComponent<Button>().interactable = enable;
        }
    }

    // Function called when a button is clicked
    // gridIndex = 1 for Grid 1, 2 for Grid 2
    // buttonIndex = index of the button clicked within the grid
    public void OnButtonClick(int gridIndex, int buttonIndex)
    {
        // If Player 1 clicked a button in Grid 1
        if (PhotonNetwork.IsMasterClient && gridIndex == 1)
        {
            // Call an RPC to update the counter for Grid 1 across all players
            photonView.RPC("UpdateCounter1", RpcTarget.All);
        }
        // If Player 2 clicked a button in Grid 2
        else if (!PhotonNetwork.IsMasterClient && gridIndex == 2)
        {
            // Call an RPC to update the counter for Grid 2 across all players
            photonView.RPC("UpdateCounter2", RpcTarget.All);
        }

        // Call an RPC to disable the button that was clicked for all players
        photonView.RPC("DisableButton", RpcTarget.All, gridIndex, buttonIndex);
    }

    // RPC function to update the counter for Grid 1
    [PunRPC]
    void UpdateCounter1()
    {
        // Increment the counter for Grid 1
        counter1++;
        // Update the counter display on the UI
        counter1Text.text = "Counter 1: " + counter1;
    }

    // RPC function to update the counter for Grid 2
    [PunRPC]
    void UpdateCounter2()
    {
        // Increment the counter for Grid 2
        counter2++;
        // Update the counter display on the UI
        counter2Text.text = "Counter 2: " + counter2;
    }

    // RPC function to disable a button after it's clicked
    [PunRPC]
    void DisableButton(int gridIndex, int buttonIndex)
    {
        // Disable a button in Grid 1
        if (gridIndex == 1)
        {
            grid1Buttons[buttonIndex].GetComponent<Button>().interactable = false;
        }
        // Disable a button in Grid 2
        else if (gridIndex == 2)
        {
            grid2Buttons[buttonIndex].GetComponent<Button>().interactable = false;
        }
    }
}
