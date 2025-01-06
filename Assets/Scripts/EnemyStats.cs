using UnityEngine;

/// <summary>
/// Immutable ScriptableObject that defines the core stats and properties of an enemy.
/// </summary>
[CreateAssetMenu(menuName = "Enemy/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    // General Information
    [Header("General")]
    [SerializeField, Tooltip("The type of enemy (e.g., Red, Blue, Green, Yellow).")]
    private EnemyType _type;
    public EnemyType Type => _type;

    [SerializeField, Tooltip("The prefab used for this enemy.")]
    private GameObject _prefab;
    public GameObject Prefab => _prefab;

    // Enemy Attributes
    [Header("Attributes")]
    [SerializeField, Tooltip("The starting speed of the enemy.")]
    private float _defaultSpeed = 10f;
    public float DefaultSpeed => _defaultSpeed;

    [SerializeField, Tooltip("The starting health of the enemy.")]
    private float _startHealth = 100f;
    public float StartHealth => _startHealth;

    [SerializeField, Tooltip("The weight of this enemy for game calculations and the number of lives it costs.")]
    private int _lives = 1;
    public int Lives => _lives;

    [SerializeField, Tooltip("The amount of money awarded for destroying this enemy.")]
    private int _worth = 1;
    public int Worth => _worth;

    // Visual Effects
    [Header("Effects")]
    [SerializeField, Tooltip("The effect displayed upon this enemy's death.")]
    private GameObject _deathEffect;
    public GameObject DeathEffect => _deathEffect;
}