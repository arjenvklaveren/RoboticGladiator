using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigEnemyButtonHandler : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    IEnumerator HitButton()
    {        
        yield return new WaitForSeconds(2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Body")
        {
            Debug.Log("HIT BUTTON");
            StartCoroutine(HitButton());
            player.GetComponent<Rigidbody>().velocity = new Vector3(player.GetComponent<Rigidbody>().velocity.x, 0, player.GetComponent<Rigidbody>().velocity.z);
            player.GetComponent<Rigidbody>().AddForce(0, player.GetComponent<MovementController>().jumpForce + (player.GetComponent<MovementController>().speed * 0.75f), 0, ForceMode.Impulse);
        }

    }
}
