using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnScreenManager : MonoBehaviour
{
    public List<GameObject> learnScreens = new List<GameObject>();
    public int currentScreen = 1;

    void Start()
    {
        try
        {
            if(GameObject.Find("MainMenuScreen").gameObject.activeSelf == false)
            {
                Debug.Log("Not active, so enable");
            }
        }  
        catch
        {
            SetActiveScreen();
        }
    }

    public void SetActiveScreen()
    {
        for(int i = 0; i < learnScreens.Count; i++)
        {
            learnScreens[i].gameObject.SetActive(false);
            if (learnScreens[i].name.Contains(currentScreen.ToString()))
            {
                learnScreens[i].gameObject.SetActive(true);
            }
            else
            {
                Debug.Log(learnScreens[i].name + " " + currentScreen);
            }
        }
    }
}
