using UnityEngine;

/// <summary>
/// Controls enemy movement along a predefined path.
/// </summary>
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Path")]
    private Transform _target;
    private int _wavepointIndex = 0;

    private Enemy _enemy;

    [Header("Movement Settings")]
    [SerializeField, Tooltip("Threshold distance to determine when to move to the next waypoint.")]
    private float waypointThreshold = 0.4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _target = Waypoints.points[_wavepointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();

        if (Vector3.Distance(transform.position, _target.position) <= waypointThreshold)
        {
            GetNextWaypoint();
        }

        // Reset speed to default
        _enemy.Speed = _enemy.stats.DefaultSpeed;
    }

    /// <summary>
    /// Moves the enemy towards the current target waypoint.
    /// </summary>
    private void MoveTowardsTarget()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _enemy.Speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Advances the enemy to the next waypoint. Ends the path if the enemy has reached the final waypoint.
    /// </summary>
    private void GetNextWaypoint()
    {
        if (_wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        _wavepointIndex++;
        _target = Waypoints.points[_wavepointIndex];
    }

    /// <summary>
    /// Sets the current waypoint index for the enemy and updates the target waypoint.
    /// </summary>
    /// <param name="index">The waypoint index to set.</param>
    public void SetWaypointIndex(int index)
    {
        _wavepointIndex = index;
        _target = Waypoints.points[_wavepointIndex];
    }

    /// <summary>
    /// Gets the current waypoint index for the enemy.
    /// </summary>
    /// <returns>The current waypoint index.</returns>
    public int GetCurrentWaypointIndex()
    {
        return _wavepointIndex;
    }

    /// <summary>
    /// Handles logic for when the enemy reaches the end of the path, including deducting lives and destroying the enemy.
    /// </summary>
    private void EndPath()
    {
        PlayerStats.Lives -= _enemy.stats.Lives;
        RoundManager.DecrementEnemiesAlive(_enemy.stats.Lives);
        Destroy(gameObject);
    }
}