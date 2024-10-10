using System.Collections;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    

    public float spawnTimerMin = 2, spawnTimerMax = 5, minSpawnX, maxSpawnX, minSpawnZ, maxSpawnZ;
    public GameObject chickenPrefab, pigPrefab, cowPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
            if (GameManager.Instance.isGameOver)
            {
                StopAllCoroutines(); 
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }

    //SpawnsEnemies with timer
    IEnumerator SpawnEnemies()
    {
        while (GameManager.Instance.isGameOver == false)
        {
            yield return new WaitForSeconds(RandomTime());
            SpawnEnemyPrefab();
        }
    }

    //spawns enemies
    void SpawnEnemyPrefab()
    {
        //Spawns Enemy
        Vector3 spawnPos = new Vector3(Random.Range(minSpawnX, maxSpawnX + 1), 
                                        0,
                                        Random.Range(minSpawnZ, maxSpawnZ + 1));

        int randomEnemyGen = Random.Range(0, 3);
        switch(randomEnemyGen)
        {
            case 0:
                Instantiate(chickenPrefab, spawnPos, Quaternion.identity);
                break;
            case 1:
                Instantiate(pigPrefab, spawnPos, Quaternion.identity);
                break;
            case 2:
                Instantiate(cowPrefab, spawnPos, Quaternion.identity);
                break;
            default:
                Debug.Log("how'd that happen");
                break;
        }
        
    }

    //Gets a random time between 2 to 5
    float RandomTime()
    {
        float randSpawnTime = Random.Range(spawnTimerMin, spawnTimerMax);
        return randSpawnTime;
    }
}
