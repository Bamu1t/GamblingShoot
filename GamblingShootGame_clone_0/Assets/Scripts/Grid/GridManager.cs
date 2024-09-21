using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

/*
 * GridManager Script:
 * 
 * This script manages two grids, each with 9 buttons. Player 1 (MasterClient) controls Grid 1, and Player 2 (non-MasterClient) controls Grid 2.
 * Each player's actions are synchronized across both players using Photon PUN 2.
 * 
 * How it works:
 * 1. The script checks if the current player is Player 1 (MasterClient) or Player 2 (non-MasterClient).
 * 2. It enables or disables buttons in the respective grids based on the player's role.
 * 3. When a player clicks a button on their grid, the button becomes unclickable for both players, and the counter for that grid is incremented.
 * 4. The updates are synchronized across the network using Photon RPC calls.
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
        if (PhotonNetwork.IsMasterClient)
        {
            // Player 1 (MasterClient) controls Grid 1
            // Enable buttons in Grid 1 and disable buttons in Grid 2
            EnableGridButtons(grid1Buttons, true);
            EnableGridButtons(grid2Buttons, false);
        }
        else
        {
            // Player 2 (non-MasterClient) controls Grid 2
            // Disable buttons in Grid 1 and enable buttons in Grid 2
            EnableGridButtons(grid1Buttons, false);
            EnableGridButtons(grid2Buttons, true);
        }
    }

    // Function to enable or disable all buttons in a grid
    void EnableGridButtons(GameObject[] gridButtons, bool enable)
    {
        foreach (GameObject button in gridButtons)
        {
            // Set the interactable property of each button
            button.GetComponent<Button>().interactable = enable;
        }
    }

    // Function called when a button is clicked
    // gridIndex = 1 for Grid 1, 2 for Grid 2
    // buttonIndex = index of the button clicked within the grid
    public void OnButtonClick(int gridIndex, int buttonIndex)
    {
        Debug.Log($"Button clicked: Grid {gridIndex}, Button {buttonIndex}");

        // If Player 1 clicked a button in Grid 1
        if (PhotonNetwork.IsMasterClient && gridIndex == 1)
        {
            Debug.Log("Calling UpdateCounter1 RPC");
            // Call an RPC to update the counter for Grid 1 across all players
            photonView.RPC("UpdateCounter1", RpcTarget.All);
        }
        // If Player 2 clicked a button in Grid 2
        else if (!PhotonNetwork.IsMasterClient && gridIndex == 2)
        {
            Debug.Log("Calling UpdateCounter2 RPC");
            // Call an RPC to update the counter for Grid 2 across all players
            photonView.RPC("UpdateCounter2", RpcTarget.All);
        }

        // Call an RPC to disable the button that was clicked for all players
        Debug.Log("Calling DisableButton RPC");
        photonView.RPC("DisableButton", RpcTarget.All, gridIndex, buttonIndex);
    }

    // RPC function to update the counter for Grid 1
    [PunRPC]
    void UpdateCounter1()
    {
        Debug.Log("UpdateCounter1 called");
        // Increment the counter for Grid 1
        counter1++;
        // Update the counter display on the UI
        counter1Text.text = "Counter 1: " + counter1;
    }

    // RPC function to update the counter for Grid 2
    [PunRPC]
    void UpdateCounter2()
    {
        Debug.Log("UpdateCounter2 called");
        // Increment the counter for Grid 2
        counter2++;
        // Update the counter display on the UI
        counter2Text.text = "Counter 2: " + counter2;
    }

    // RPC function to disable a button after it's clicked
    [PunRPC]
    void DisableButton(int gridIndex, int buttonIndex)
    {
        Debug.Log($"DisableButton called: Grid {gridIndex}, Button {buttonIndex}");
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
