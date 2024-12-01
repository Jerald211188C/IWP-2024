using UnityEngine;
using System.Collections;
using TMPro;

public class RoundController : MonoBehaviour
{
    [SerializeField] private int _StartingRound = 1;
    [SerializeField] private int _CurrentRound;
    [SerializeField] private GameObject _EnemyPrefab;
    [SerializeField] private Transform _SpawnPoint1;
    [SerializeField] private Transform _SpawnPoint2;
    [SerializeField] private Transform _SpawnPoint3;
    [SerializeField] private Transform _SpawnPoint4;
    [SerializeField] TextMeshProUGUI _Roundtext;
    public int _EnemyCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _CurrentRound = _StartingRound;
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) // When there are no enemies left in the scene
        {
            EndOfRound();
        }

        if (_EnemyCount <= 0)
        {
            EndOfRound();
            StartRound();
        }
    }

    private void EndOfRound()
    {
        _CurrentRound++;
        _Roundtext.text = "Wave: " + _CurrentRound.ToString();
        BossRround();
    }

    private void BossRround()
    {
        int _BossRound;
        _BossRound = _CurrentRound / 10;

        if (_CurrentRound % 10 == 0)
        {
            Debug.Log("Boss Round");
        }
    }

    private void StartRound()
    {
        // Calculate the enemy count using the Fibonacci sequence
        _EnemyCount = GetFibonacciNumber(_CurrentRound);

        // Spawn the enemies
        for (int i = 0; i < _EnemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Create an array of all spawn points
        Transform[] spawnPoints = new Transform[] { _SpawnPoint1, _SpawnPoint2, _SpawnPoint3, _SpawnPoint4 };

        // Select a random spawn point
        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the enemy at the selected spawn point
        Instantiate(_EnemyPrefab, selectedSpawnPoint.position, selectedSpawnPoint.rotation);
    }

    // Fibonacci sequence method
    private int GetFibonacciNumber(int n)
    {
        if (n <= 1)
            return n;

        int a = 0, b = 1;
        for (int i = 2; i <= n; i++)
        {
            int temp = a + b;
            a = b;
            b = temp;
        }
        return b;
    }

    private IEnumerator NextRound()
    {
        yield return new WaitForSeconds(2f); // Wait for the reload duration
        StartRound();
    }
}
