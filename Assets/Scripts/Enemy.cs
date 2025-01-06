using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents an enemy with health, speed, and behavior upon death.
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [Tooltip("The stats assigned to this enemy type.")]
    public EnemyStats stats;

    [Header("UI")]
    [Tooltip("The health bar UI element.")]
    public Image healthBar;

    private float _health;
    private float _speed;
    private bool _isDead = false;

    /// <summary>
    /// Initializes the enemy with its stats.
    /// </summary>
    /// <param name="stats">The stats to assign.</param>
    public void Init(EnemyStats stats)
    {
        this.stats = stats;
        _health = stats.StartHealth;
        _speed = stats.DefaultSpeed;
    }

    /// <summary>
    /// Current speed of the enemy.
    /// </summary>
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    /// <summary>
    /// Reduces the enemy's speed by a percentage.
    /// </summary>
    /// <param name="pct">The percentage to reduce speed by.</param>
    public void Slow(float pct)
    {
        _speed = stats.DefaultSpeed * (1f - pct);
    }

    /// <summary>
    /// Applies damage to the enemy and checks for death.
    /// </summary>
    /// <param name="amount">The damage to apply.</param>
    public void TakeDamage(float amount)
    {
        if (_isDead) return;

        _health -= amount;
        Debug.Log($"{stats.Type} took {amount} damage. Remaining health: {_health}");

        UpdateHealthBar();

        if (_health <= 0)
        {
            float excessDamage = Mathf.Abs(_health);
            Die(excessDamage);
        }
    }

    /// <summary>
    /// Handles the logic for when the enemy dies, including decrementing the alive enemy count,
    /// spawning weaker enemies if applicable, and triggering death effects.
    /// </summary>
    /// <param name="excessDamage">The remaining damage after the enemy's health reaches zero.</param>
    private void Die(float excessDamage = 0)
    {
        if (_isDead) return;
        _isDead = true;

        Debug.Log($"Enemy {stats.Type} is dying. Excess damage: {excessDamage}");

        PlayerStats.Money += stats.Worth;
        
        RoundManager.DecrementEnemiesAlive(stats.Lives);

        SpawnWeakerEnemy(excessDamage);

        if (stats.DeathEffect != null)
        {
            GameObject effect = Instantiate(stats.DeathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Spawns the next weaker enemy type at the current enemy's position.
    /// Passes the remaining excess damage to the weaker enemy and adjusts its health accordingly.
    /// </summary>
    /// <param name="excessDamage">The remaining damage to apply to the weaker enemy</param>
    private void SpawnWeakerEnemy(float excessDamage)
    {
        var weakerType = GetWeakerEnemyType(stats.Type);
        if (weakerType == null)
        {
            Debug.Log($"No weaker enemy exists for {stats.Type}.");
            return;
        }

        EnemyStats weakerStats = RoundManager.GetEnemyStats(weakerType.Value);
        if (weakerStats == null)
        {
            Debug.LogError($"Failed to find stats for weaker enemy: {weakerType}");
            return;
        }

        Debug.Log($"Spawning weaker enemy: {weakerType} at position {transform.position} with excess damage: {excessDamage}");

        GameObject newEnemy = Instantiate(weakerStats.Prefab, transform.position, Quaternion.identity);
        Enemy weakerEnemy = newEnemy.GetComponent<Enemy>();
        if (weakerEnemy == null)
        {
            Debug.LogError($"Failed to spawn weaker enemy: {weakerType}. Prefab is missing Enemy script.");
            return;
        }

        weakerEnemy.Init(weakerStats);

        EnemyMovement currentMovement = GetComponent<EnemyMovement>();
        EnemyMovement weakerMovement = newEnemy.GetComponent<EnemyMovement>();
        if (currentMovement != null && weakerMovement != null)
        {
            weakerMovement.SetWaypointIndex(currentMovement.GetCurrentWaypointIndex());
        }

        float remainingHealth = weakerStats.StartHealth - excessDamage;
        if (remainingHealth > 0)
        {
            weakerEnemy.TakeDamage(excessDamage);
        }
        else
        {
            Debug.LogWarning($"{weakerType} is destroyed immediately by excess damage. Spawning next weaker enemy.");
            weakerEnemy.Die(Mathf.Abs(remainingHealth));
        }
    }

    /// <summary>
    /// Determines the next weaker enemy type based on the current enemy type.
    /// </summary>
    /// <param name="currentType">The next weaker enemy type, or null if no weaker enemy exists.</param>
    /// <returns></returns>
    private EnemyType? GetWeakerEnemyType(EnemyType currentType)
    {
        return currentType switch
        {
            EnemyType.Yellow => EnemyType.Green,
            EnemyType.Green => EnemyType.Blue,
            EnemyType.Blue => EnemyType.Red,
            _ => null
        };
    }

    /// <summary>
    /// Updates the enemy's health bar UI to reflect the current health.
    /// </summary>
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = _health / stats.StartHealth;
        }
    }
}