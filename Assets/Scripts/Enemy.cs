using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents an enemy with health, speed, and behavior upon death.
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [Tooltip("The stats assigned to this enemy type.")]
    public EnemyStats stats; // Scriptable object for stats

    [Header("UI")]
    [Tooltip("The health bar UI element.")]
    public Image healthBar;
    
    private float _health;
    private float _speed;
    private bool _isDead = false;

    public delegate void EnemyDiedHandler(Enemy enemy);
    public static event EnemyDiedHandler OnEnemyDied;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = stats.startHealth;
        _speed = stats.defaultSpeed;
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
    /// Slows the enemy by a given percentage.
    /// </summary>
    /// <param name="pct">Percentage to reduce speed.</param>
    public void Slow(float pct)
    {
        _speed = stats.defaultSpeed * (1f - pct);
    }

    /// <summary>
    /// Reduces the enemy's health by a given amount and checks for death.
    /// </summary>
    /// <param name="amount">The amount of damage to apply.</param>
    public void TakeDamage(float amount)
    {
        _health -= amount;
        UpdateHealthBar();

        if (_health <= 0 && !_isDead)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = _health / stats.startHealth;
        }
    }

    private void Die()
    {
        _isDead = true;

        // Notify listeners
        OnEnemyDied?.Invoke(this);

        // Spawn death effect
        if (stats.deathEffect != null)
        {
            GameObject effect = Instantiate(stats.deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
        }

        Destroy(gameObject);
    }
}