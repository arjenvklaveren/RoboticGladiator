using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAudience : MonoBehaviour
{
    public GameObject spectatorPrefab;

    void Start()
    {
        foreach (Transform seat in transform.GetChild(0))
        {
            float spectatorMargin = -0.43f;
            for (int i = 0; i < 7; i++)
            {
                int spectatorOdds = Random.Range(0, 100);               
                if(spectatorOdds < 75)
                {
                    GameObject spectator = Instantiate(spectatorPrefab, seat.transform.position, seat.transform.rotation);
                    spectator.transform.SetParent(seat);
                    spectator.transform.localPosition = new Vector3(spectatorMargin, 0.75f, 0);
                }
                spectatorMargin += 0.1228f; 
            }
        }
    }
}
