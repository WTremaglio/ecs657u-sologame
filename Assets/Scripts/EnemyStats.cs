using UnityEngine;

/// <summary>
/// ScriptableObject representing enemy stats.
/// </summary>
[CreateAssetMenu(menuName = "Enemy/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [Header("General")]
    [Tooltip("The type of enemy.")]
    public EnemyType type;

    [Tooltip("The prefab for this enemy.")]
    public GameObject prefab;

    [Header("Attributes")]
    [Tooltip("The starting speed of the enemy")]
    public float defaultSpeed = 10f;

    [Tooltip("The starting health of the enemy")]
    public float startHealth = 100f;

    [Tooltip("The amount of money awarded for destroyiing this enemy.")]
    public int worth = 50;

    [Header("Effects")]
    [Tooltip("The effect spawned upon this enemy's death.")]
    public GameObject deathEffect;
}