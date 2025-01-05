using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the rounds in the game, including spawning enemies and tracking progression.
/// </summary>
public class RoundManager : MonoBehaviour
{
    [Header("General Settigns")]
    [Tooltip("The spawn point for the enemies.")]
    public Transform spawnPoint;

    [Tooltip("Time between rounds in seconds.")]
    public float timeBetweenRounds = 5.5f;

    [Tooltip("The game manager responsible for win/lose conditions.")]
    public TMP_Text roundCountdownText;

    [Tooltip("The game manager responsible for win/lose conditions.")]
    public GameManager gameManager;

    [Header("Enemy Settings")]
    [Tooltip("List of enemy stats for all enemy types.")]
    public EnemyStats[] enemyStatsList;
    
    private static int _enemiesAlive = 0;
    
    private float _countdown = 2f;
    private int _roundIndex = 0;
    private Round[] _rounds;

    public static int EnemiesAlive
    {
        get => _enemiesAlive;
        private set => _enemiesAlive = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rounds = RoundData.GetRounds();
        EnemiesAlive = 0;

        Enemy.OnEnemyDied += HandleEnemyDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemiesAlive > 0) return;

        if (_roundIndex == _rounds.Length)
        {
            gameManager.WinLevel();
            enabled = false;
            return;
        }

        if (_countdown <= 0f)
        {
            PlayerStats.Rounds++;
            StartCoroutine(SpawnRound(_rounds[_roundIndex]));
            _countdown = timeBetweenRounds;
            _roundIndex++;
            return;
        }

        _countdown -= Time.deltaTime;
        _countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);

        roundCountdownText.text = $"{_countdown:00.00}";
    }

    // Unsubscribe from events when disabled
    void OnDisable()
    {
        Enemy.OnEnemyDied -= HandleEnemyDeath;
    }

    /// <summary>
    /// Spawns all enemies for the given round.
    /// </summary>
    /// <param name="round">The round configuration.</param>
    /// <returns>Coroutine for spawning enemies.</returns>
    IEnumerator SpawnRound(Round round)
    {
        // Calculate the total enemies in this round
        foreach (var spawnInfo in round.enemies)
        {
            EnemiesAlive += spawnInfo.count;
        }

        // Spawn enemies according to the round configuration
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
    void SpawnEnemy(EnemyType type)
    {
        EnemyStats enemyStats = GetEnemyStats(type);
        if (enemyStats != null)
        {
            GameObject enemyPrefab = Instantiate(enemyStats.prefab, spawnPoint.position, spawnPoint.rotation);
            Enemy enemy = enemyPrefab.GetComponent<Enemy>();
            enemy.stats = enemyStats;
        }
    }

    /// <summary>
    /// Retrieves the stats for a specific enemy type.
    /// </summary>
    /// <param name="type">The type of enemy to find.</param>
    /// <returns>The corresponding EnemyStats, or null if not found.</returns>
    EnemyStats GetEnemyStats(EnemyType type)
    {
        foreach (EnemyStats stats in enemyStatsList)
        {
            if (stats.type == type) return stats;
        }
        return null;
    }

    /// <summary>
    /// Handles logic for when an enemy dies.
    /// </summary>
    /// <param name="enemy">The enemy that died.</param>
    void HandleEnemyDeath(Enemy enemy)
    {
        EnemiesAlive--;
    }

    /// <summary>
    /// Decrements the count of enemies alive.
    /// </summary>
    public static void DecrementEnemiesAlive()
    {
        if (_enemiesAlive > 0)
        {
            _enemiesAlive--;
        }
    }
}