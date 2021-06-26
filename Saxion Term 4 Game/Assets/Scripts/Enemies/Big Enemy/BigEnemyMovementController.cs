using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigEnemyMovementController : MonoBehaviour
{
    GameObject player;
    GameObject head;

    public NavMeshAgent agent;

    void Start()
    {
        player = GameObject.Find("Player");
        head = transform.GetChild(0).gameObject;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(5, 100);
    }

    private void FixedUpdate()
    {
        RotateHead();
        agent.SetDestination(player.transform.position);
    }


    void RotateHead()
    {
        Vector3 relativePos = (player.transform.position + new Vector3(0, -3, 0)) - head.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        head.transform.rotation = Quaternion.Lerp(head.transform.rotation, toRotation, (0.005f + Vector3.Distance(transform.position, player.transform.position)) / 2000);
    }
}
