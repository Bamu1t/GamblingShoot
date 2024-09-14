using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] playPanels;

    public void PlayMenuPanels(GameObject activePanel){

        for (int i = 0; i < playPanels.Length; i++){
            playPanels[i].SetActive(false);
        }
        activePanel.SetActive(true);
    }
}
