using UnityEngine;

/// <summary>
/// Manages turret functionality, including targeting, firing, and turret-specific behaviors.
/// </summary>
public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    /// <summary>
    /// Type of the turret (e.g., Standard, Cannon, Laser, Sniper).
    /// </summary>
    public TurretType turretType;

    // General settings
    public float range = 15f;
    public float fireRate = 1f;
    private float _fireCooldown = 0f;

    // Visuals and effects
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private ParticleSystem _fireEffect;
    [SerializeField] private LineRenderer _laserBeam;
    [SerializeField] private Light _impactLight;
    [SerializeField] private ParticleSystem _impactEffect;

    // Laser-specific settings
    [SerializeField] private int _laserDamage = 30;
    [SerializeField] private float _slowAmount = 0.5f;

    // Sniper-specific settings
    [SerializeField] private bool _infiniteRange = false;
    [SerializeField] private float _sniperDamage = 100f;
    
    // Rotation settings
    [SerializeField] private Transform _rotationPivot;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _turnSpeed = 10f;

    // Enemy tag
    [SerializeField] private string _enemyTag = "Enemy";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (turretType == TurretType.Laser && _laserBeam.enabled)
            {
                DisableLaser();
            }
            return;
        }

        if (turretType != TurretType.Sniper)
        {
            LockOnTarget();
        }

        if (turretType == TurretType.Laser)
        {
            FireLaser();
        }
        else if (turretType == TurretType.Sniper)
        {
            AttackSniper();
        }
        else if (_fireCooldown <= 0f)
        {
            Shoot();
            _fireCooldown = 1f / fireRate;
        }

        _fireCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// Updates the current target by searching for the nearest enemy within range.
    /// </summary>
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(_enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        target = (nearestEnemy != null && (shortestDistance <= range || _infiniteRange)) 
         ? nearestEnemy.transform 
         : null;
         targetEnemy = target?.GetComponent<Enemy>();
    }

    /// <summary>
    /// Locks the turret's rotation onto the target
    /// </summary>
    private void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_rotationPivot.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;
        _rotationPivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    /// <summary>
    /// Fires a projectile at the target.
    /// </summary>
    private void Shoot()
    {
        if (_bulletPrefab != null)
        {
            GameObject bulletGO = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Seek(target);
            }
        }

        if (_fireEffect != null && (turretType == TurretType.Standard || turretType == TurretType.Cannon))
        {
            _fireEffect.Play();
        }
    }

    /// <summary>
    /// Fires a laser at the target.
    /// </summary>
    private void FireLaser()
    {
        // Set laser positions
        _laserBeam.SetPosition(0, _firePoint.position);
        _laserBeam.SetPosition(1, target.position);

        // Calculate direction
        Vector3 direction = _firePoint.position - target.position;

        // Update impact effect position and rotation
        if (_impactEffect != null)
        {
            _impactEffect.transform.position = target.position;
            _impactEffect.transform.rotation = Quaternion.LookRotation(-direction);
        }

        // Deal damage and apply slow effect
        targetEnemy.TakeDamage(_laserDamage * Time.deltaTime);
        targetEnemy.Slow(_slowAmount);

        // Enable laser visuals if not already active
        if (!_laserBeam.enabled)
        {
            _laserBeam.enabled = true;
            if (_impactEffect != null) _impactEffect.Play();
            if (_impactLight != null) _impactLight.enabled = true;
        }
    }

    /// <summary>
    /// Disables the laser visual effects.
    /// </summary>
    private void DisableLaser()
    {
        _laserBeam.enabled = false;
        if (_impactLight != null) _impactLight.enabled = false;
        if (_impactEffect != null) _impactEffect.Stop();
    }

    /// <summary>
    /// Attacks the target with a sniper shot.
    /// </summary>
    private void AttackSniper()
    {
        if (_fireCooldown <= 0f)
        {
            targetEnemy.TakeDamage(_sniperDamage);
            _fireCooldown = 1f / fireRate;
        }
    }

    /// <summary>
    /// Draws the turret's range in the editor.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}