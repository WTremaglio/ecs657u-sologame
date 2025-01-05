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
    
    [Tooltip("Threshold distance to determine when to move to the next waypoint.")]
    private const float WaypointThreshold = 0.4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _target = Waypoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();

        if (Vector3.Distance(transform.position, _target.position) <= WaypointThreshold)
        {
            GetNextWaypoint();
        }

        // Reset speed to default
        _enemy.Speed = _enemy.stats.defaultSpeed;
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _enemy.Speed * Time.deltaTime, Space.World);
    }

    void GetNextWaypoint()
    {
        if (_wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        _wavepointIndex++;
        _target = Waypoints.points[_wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        RoundManager.DecrementEnemiesAlive();
        Destroy(gameObject);
    }
}