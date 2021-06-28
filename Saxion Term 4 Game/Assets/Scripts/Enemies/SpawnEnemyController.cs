using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class SpawnEnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject spawnCubesHolder;
    public List<GameObject> spawnCubeList;

    [SerializeField]
    private int currentSpawner = 0;
    [SerializeField]
    private int currentSpawnAmount = 1;

    public int kills;
    public TextMeshProUGUI killsText;


    void Start()
    {
        kills = 0;
        enemyPrefab.GetComponent<NavMeshAgent>().speed = 5;
        enemyPrefab.GetComponent<NavMeshAgent>().angularSpeed = 100;
        enemyPrefab.GetComponent<NavMeshAgent>().acceleration = 5;

        foreach (Transform spawnCube in spawnCubesHolder.transform)
        {
            spawnCubeList.Add(spawnCube.gameObject);
        }
        SpawnEnemies();
        SetKillText();
    }

    public void SetKillText()
    {
        killsText.text = "Kills: " + kills.ToString();
    }

    public void SpawnEnemies()
    {
        if (currentSpawnAmount < 4)
        {
            for (int i = 0; i < currentSpawnAmount; i++)
            {
                Instantiate(enemyPrefab, spawnCubeList[currentSpawner].transform.position, spawnCubeList[currentSpawner].transform.rotation);
                if(currentSpawner < 3)
                {
                    currentSpawner++;
                }
                else
                {
                    currentSpawner = 0;
                }
                
            }
            currentSpawnAmount++;
        }
        else
        {
            Instantiate(enemyPrefab, spawnCubeList[currentSpawner].transform.position, spawnCubeList[currentSpawner].transform.rotation);
            if (currentSpawner < 3)
            {
                currentSpawner++;
            }
            else
            {
                currentSpawner = 0;
            }           
        }
        enemyPrefab.GetComponent<NavMeshAgent>().speed += 1;
        enemyPrefab.GetComponent<NavMeshAgent>().acceleration += 0.5f;
        enemyPrefab.GetComponent<NavMeshAgent>().angularSpeed += 3;
        enemyPrefab.GetComponent<BigEnemyAttackController>().armRotationSpeed += 0.05f;
        enemyPrefab.GetComponent<BigEnemyAttackController>().armRotationStartDistance += 3;
    }
}
