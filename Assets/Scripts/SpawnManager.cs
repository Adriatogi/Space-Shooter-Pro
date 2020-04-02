using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private float spawnRate = 5.0f;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUps;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void startSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
        {
            //Spawn enemy
            Vector3 randomSpawn = new Vector3(Random.Range(-9f, 9f), 7.06f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, randomSpawn, Quaternion.identity);
            //Child the object
            newEnemy.transform.parent = _enemyContainer.transform;

            //Wait
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
        {
            //Wait
            yield return new WaitForSeconds(Random.Range(7.0f, 11.0f));

            //Spawn Powerup
            Vector3 randomSpawn = new Vector3(Random.Range(-9f, 9f), 7.06f, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUps[randomPowerUp], randomSpawn, Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
