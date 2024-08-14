using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabsManager : MonoBehaviour
{
    public GameObject[] Tabs;
    public Image[] TabButtons;
    public Sprite InactiveTabBG, ActiveTabBG;
    public Vector2 InnactiveTabButtonsSize, ActiveTabButtonsSize;
    //public int InnactiveTabButtonsPosition, ActiveTabButtonsPosition;

    public void SwitchingToTab(int TabID){
        foreach (GameObject go in Tabs){
            go.SetActive(false);
        }
        Tabs[TabID].SetActive(true);

        foreach(Image im in TabButtons){
            im.sprite = InactiveTabBG;
            im.rectTransform.sizeDelta = InnactiveTabButtonsSize;
        }
        TabButtons[TabID].sprite = ActiveTabBG;
        TabButtons[TabID].rectTransform.sizeDelta = ActiveTabButtonsSize;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
