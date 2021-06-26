using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    LearnScreenManager SCM;

    public GameObject mainMenuScreen;
    public GameObject mainMenuScreenBackGround;

    public GameObject learnToPlayScreen;
    public GameObject learnToPlayScreenBackGround;

    private void Start()
    {
        SCM = this.gameObject.GetComponent<LearnScreenManager>();
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickLearnButton()
    {
        mainMenuScreen.SetActive(false);
        mainMenuScreenBackGround.SetActive(false);
        learnToPlayScreen.SetActive(true);
        learnToPlayScreenBackGround.SetActive(true);
        SCM.currentScreen = 1;
        SCM.SetActiveScreen();
    }

    public void OnClickNextButton()
    {
        SCM.currentScreen++;
        SCM.SetActiveScreen();

        if(SCM.currentScreen == SCM.learnScreens.Count + 1)
        {
            mainMenuScreen.SetActive(false);
            mainMenuScreenBackGround.SetActive(false);
            learnToPlayScreen.SetActive(false);
            learnToPlayScreenBackGround.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }

    public void OnClickBackButton()
    {
        SCM.currentScreen--;
        SCM.SetActiveScreen();

        if (SCM.currentScreen == 0)
        {
            mainMenuScreen.SetActive(true);
            mainMenuScreenBackGround.SetActive(true);
            learnToPlayScreen.SetActive(false);
            learnToPlayScreenBackGround.SetActive(false);
        }
    }
}
