using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavCodingWay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] panels;

    public void NavigationBarClick(GameObject activePlayPanel){

        for (int i = 0; i < panels.Length; i++){
            panels[i].SetActive(false);
        }
        activePlayPanel.SetActive(true);
    }
}
