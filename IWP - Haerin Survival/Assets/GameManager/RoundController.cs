using UnityEngine;
using System.Collections;
using TMPro;

public class RoundController : MonoBehaviour
{
    [SerializeField] private int _StartingRound = 1;
    [SerializeField] private int _CurrentRound;
    [SerializeField] private GameObject _EnemyPrefab;
    [SerializeField] private GameObject _BossPreFab;
    [SerializeField] private Transform _SpawnPoint1;
    [SerializeField] private Transform _SpawnPoint2;
    [SerializeField] private Transform _SpawnPoint3;
    [SerializeField] private Transform _SpawnPoint4;
    [SerializeField] private TextMeshProUGUI _Roundtext;
    public int _EnemyCount;

    private bool _isRoundActive = false;

    void Start()
    {
        _CurrentRound = _StartingRound;
        StartRound();
    }

    void Update()
    {
        if (_EnemyCount <= 0 && _isRoundActive)
        {
            EndOfRound();
        }
    }

    private void EndOfRound()
    {
        _isRoundActive = false; // Prevent overlapping rounds
        Debug.Log("Before" + _CurrentRound);
        _CurrentRound++;
        Debug.Log("After" + _CurrentRound);
        _Roundtext.text = "Wave: " + _CurrentRound.ToString();

        // Check if it's a boss round
        if (_CurrentRound % 10 == 0)
        {
            BossRround();
        }
        else
        {
            StartCoroutine(NextRound());
        }
    }

    private void BossRround()
    {
        // Choose a random spawn point for the boss
        Transform[] spawnPoints = new Transform[] { _SpawnPoint1, _SpawnPoint2, _SpawnPoint3, _SpawnPoint4 };
        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the boss at the selected spawn point
        Instantiate(_BossPreFab, selectedSpawnPoint.position, selectedSpawnPoint.rotation);
        Debug.Log("Boss Round");

        // Start the next round after the boss is defeated
        StartCoroutine(NextRound());
    }

    private void StartRound()
    {
        _isRoundActive = true;

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
        yield return new WaitForSeconds(2f); // Wait before starting the next round
        StartRound();
    }
}