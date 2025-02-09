using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    //objects to spawn
    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerUpsPrefab;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartSpawnRountines();
    }

    // Update is called once per frame
     public void StartSpawnRountines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }
    //spawn enemy every 5 seconds

   IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.GameOver == false)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while (_gameManager.GameOver == false)
        {
            int RandomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUpsPrefab[RandomPowerUp], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
       
    }
}
