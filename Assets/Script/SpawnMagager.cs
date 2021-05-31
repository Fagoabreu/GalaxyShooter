using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static ControlsInput;

public class SpawnMagager : MonoBehaviour
{
    [SerializeField]
    private float enemySpawnTime = 5f;
    [SerializeField]
    private GameObject enemyShip = null;

    [SerializeField]
    private float powerupSpawnTime = 10f;
    [SerializeField]
    private GameObject[] powerups = null;

    //comunicacao
    private GameManager _gameManager;

    public void Awake()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    void Start()
    {
        StartSpawnCorroutines();
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            
            Instantiate(enemyShip, new Vector3(Random.Range(-8f, 8f), 6.12f, 0), Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnTime);
        }

    }

    private IEnumerator PowerUpSpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            int randomPowerup = Random.Range(0, powerups.Length);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(powerupSpawnTime);
        }

    }


   public void StartSpawnCorroutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

}
