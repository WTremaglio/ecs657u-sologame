using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the rounds in the game, including spawning enemies and tracking progression.
/// </summary>
public class RoundManager : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField, Tooltip("The spawn point for the enemies.")]
    private Transform _spawnPoint;

    [SerializeField, Tooltip("The game manager responsible for win/lose conditions.")]
    private GameManager _gameManager;

    [Header("Enemy Settings")]
    [SerializeField, Tooltip("List of enemy stats for all enemy types.")]
    private EnemyStats[] _enemyStatsList;

    private static int _enemiesAlive = 0;

    private int _roundIndex = 0;
    private bool _roundInProgress = false;
    private Round[] _rounds;

    public bool RoundInProgress => _roundInProgress;

    /// <summary>
    /// Tracks the number of alive enemies in the current round.
    /// </summary>
    public static int EnemiesAlive
    {
        get => _enemiesAlive;
        private set => _enemiesAlive = value;
    }

    /// <summary>
    /// Singleton instance of the RoundManager.
    /// </summary>
    public static RoundManager Instance { get; private set; }

    /// <summary>
    /// Ensures the singleton instance of the RoundManager is properly initialized.
    /// Destroys duplicate instances to maintain a single active RoundManager.
    /// </summary>
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rounds = RoundData.GetRounds();
        EnemiesAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemiesAlive > 0) return;

        if (_roundIndex == _rounds.Length)
        {
            _gameManager.WinLevel();
            enabled = false;
            return;
        }

        if (_roundInProgress && EnemiesAlive == 0)
        {
            _roundInProgress = false;
        }
    }

    /// <summary>
    /// Starts the next round when called.
    /// </summary>
    public void StartNextRound()
    {
        if (_roundIndex >= _rounds.Length)
        {
            _gameManager.WinLevel();
            enabled = false;
            return;
        }

        if (_roundInProgress)
        {
            return;
        }

        _roundInProgress = true;
        PlayerStats.Rounds++;
        StartCoroutine(SpawnRound(_rounds[_roundIndex]));
        _roundIndex++;
    }

    /// <summary>
    /// Spawns all enemies for the given round.
    /// </summary>
    /// <param name="round">The round configuration.</param>
    /// <returns>Coroutine for spawning enemies.</returns>
    private IEnumerator SpawnRound(Round round)
    {
        // Calculate total weighted enemies for this round
        foreach (var spawnInfo in round.enemies)
        {
            var stats = GetEnemyStats(spawnInfo.enemyType);
            if (stats != null)
            {
                EnemiesAlive += spawnInfo.count * stats.Lives;
                Debug.Log($"Adding {spawnInfo.count * stats.Lives} to EnemiesAlive for {spawnInfo.enemyType}. Total: {EnemiesAlive}");
            }
            else
            {
                Debug.LogError($"Failed to find stats for enemy type: {spawnInfo.enemyType}");
            }
        }

        // Spawn enemies
        foreach (var spawnInfo in round.enemies)
        {
            for (int i = 0; i < spawnInfo.count; i++)
            {
                SpawnEnemy(spawnInfo.enemyType);
                yield return new WaitForSeconds(spawnInfo.delayBetween);
            }
        }
    }

    /// <summary>
    /// Spawns a single enemy of the given type.
    /// </summary>
    /// <param name="type">The type of enemy to spawn.</param>
    private void SpawnEnemy(EnemyType type)
    {
        EnemyStats enemyStats = GetEnemyStats(type);
        if (enemyStats != null)
        {
            GameObject newEnemy = Instantiate(enemyStats.Prefab, _spawnPoint.position, _spawnPoint.rotation);
            Enemy enemy = newEnemy.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Init(enemyStats); // Use Init to set up stats and health immediately
            }
            else
            {
                Debug.LogError($"Spawned prefab for {type} is missing the Enemy script!");
            }
        }
        else
        {
            Debug.LogError($"Failed to find stats for enemy type: {type}");
        }
    }

    /// <summary>
    /// Retrieves the stats for a specific enemy type.
    /// </summary>
    /// <param name="type">The type of enemy to find.</param>
    /// <returns>The corresponding EnemyStats, or null if not found.</returns>
    public static EnemyStats GetEnemyStats(EnemyType type)
    {
        foreach (EnemyStats stats in Instance._enemyStatsList)
        {
            if (stats.Type == type) return stats;
        }
        return null;
    }

    /// <summary>
    /// Decrements the count of enemies alive.
    /// </summary>
    /// <param name="weight">The weight of the enemy being removed.</param>
    public static void DecrementEnemiesAlive(int weight = 1)
    {
        if (_enemiesAlive > 0)
        {
            _enemiesAlive -= weight;
            Debug.Log($"Decrementing EnemiesAlive by {weight}. Remaining: {_enemiesAlive}");
        }
        else
        {
            Debug.LogWarning("Attempted to decrement EnemiesAlive when it was already 0.");
        }
    }
}