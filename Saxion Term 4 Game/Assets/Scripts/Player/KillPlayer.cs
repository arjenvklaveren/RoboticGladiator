using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public Image blackScreen;
    public Image fakeMainScreen;
    public AudioSource deathSound;

    float fadeAlpha = 1;

    bool isDead;

    bool canPlayDeathSound;

    private void Start()
    {
        canPlayDeathSound = true;
    }

    public void Kill()
    {
        StartCoroutine(KillSequence());
    }

    private void FixedUpdate()
    {
        if(isDead)
        {
            if(fadeAlpha <= 1)
            {
                fadeAlpha -= 0.01f;
            }
            blackScreen.color = new Color(0,0,0, fadeAlpha);
            if(fadeAlpha < 0.01)
            {
                deathSound.volume = 0;
                deathSound.Stop();
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator KillSequence()
    {
        if (canPlayDeathSound)
        {
            deathSound.Play();
            canPlayDeathSound = false;
        }
        else
        {
            deathSound.volume = 0;
        }
        deathSound.loop = false;

        blackScreen.gameObject.SetActive(true);
        fakeMainScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        isDead = true;
    }
}
