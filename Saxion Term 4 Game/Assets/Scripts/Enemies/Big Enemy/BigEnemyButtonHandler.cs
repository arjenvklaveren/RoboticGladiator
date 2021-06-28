using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigEnemyButtonHandler : MonoBehaviour
{
    private GameObject player;

    public GameObject mainParent;

    public GameObject hitButton;
    public GameObject chestPart;
    public GameObject headPart;
    public GameObject armsPart;

    public GameObject explosion;

    private GameObject spawnEnemyController;

    public AudioSource enemyAudio;
    public AudioClip bellClip;
    public AudioClip explosionClip;

    private void Start()
    {
        player = GameObject.Find("Player");
        spawnEnemyController = GameObject.Find("SpawnEnemyController");
    }

    public IEnumerator HitButton()
    {
        //Kill "Animation"
        enemyAudio.clip = bellClip;
        enemyAudio.volume = 0.2f;
        enemyAudio.Play();
        mainParent.GetComponent<BigEnemyMovementController>().enabled = false;
        mainParent.GetComponent<BigEnemyAttackController>().enabled = false;
        Destroy(mainParent.GetComponent<NavMeshAgent>());

        yield return new WaitForSeconds(2);

        enemyAudio.clip = explosionClip;
        enemyAudio.volume = 0.5f;
        enemyAudio.Play();
        explosion.SetActive(true);
        hitButton.AddComponent<Rigidbody>();
        chestPart.AddComponent<Rigidbody>();
        headPart.AddComponent<Rigidbody>();
        armsPart.AddComponent<Rigidbody>();

        spawnEnemyController.GetComponent<SpawnEnemyController>().SpawnEnemies();

        yield return new WaitForSeconds(5);

        Destroy(mainParent);
        spawnEnemyController.GetComponent<SpawnEnemyController>().kills++;
        spawnEnemyController.GetComponent<SpawnEnemyController>().SetKillText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Body")
        {
            StartCoroutine(HitButton());
            player.GetComponent<Rigidbody>().velocity = new Vector3(player.GetComponent<Rigidbody>().velocity.x, 0, player.GetComponent<Rigidbody>().velocity.z);
            player.GetComponent<Rigidbody>().AddForce(0, player.GetComponent<MovementController>().jumpForce + (player.GetComponent<MovementController>().speed * 0.75f), 0, ForceMode.Impulse);
        }

    }
}
