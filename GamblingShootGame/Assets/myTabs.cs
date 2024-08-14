using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class myTabs : MonoBehaviour
{

    public GameObject BTN_hometab;
    public GameObject BTN_playtab;
    public GameObject BTN_customtab;
    public GameObject BTN_shoptab;

    public GameObject homeTab;
    public GameObject playTab;
    public GameObject customTab;
    public GameObject shopTab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllTabs(){
        homeTab.SetActive(false);
        playTab.SetActive(false);
        customTab.SetActive(false);
        shopTab.SetActive(false);
    }

    public void ShowHomeTab(){
        HideAllTabs();
        homeTab.SetActive(true);
    }

    public void ShowPlayTab(){
        HideAllTabs();
        playTab.SetActive(true);
    }

    public void ShowCustomTab(){
        HideAllTabs();
        customTab.SetActive(true);
    }

    public void ShowShopTab(){
        HideAllTabs();
        shopTab.SetActive(true);
    }
}
