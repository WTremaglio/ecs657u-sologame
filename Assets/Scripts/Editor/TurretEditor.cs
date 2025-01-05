using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for the Turret component.
/// </summary>
[CustomEditor(typeof(Turret))]
public class TurretEditor : Editor
{
    private SerializedProperty turretType;

    // General settings
    private SerializedProperty _range;
    private SerializedProperty _fireRate;

    // Visuals and effects
    private SerializedProperty _bulletPrefab;
    private SerializedProperty _fireEffect;
    private SerializedProperty _laserBeam;
    private SerializedProperty _impactLight;
    private SerializedProperty _impactEffect;

    // Laser-specific settings
    private SerializedProperty _laserDamage;
    private SerializedProperty _slowAmount;

    // Sniper-specific settings
    private SerializedProperty _infiniteRange;
    private SerializedProperty _sniperDamage;

    // Rotation and Firing
    private SerializedProperty _rotationPivot;
    private SerializedProperty _firePoint;
    private SerializedProperty _turnSpeed;

    /// <summary>
    /// Called when the editor is enabled. Initializes serialized properties.
    /// </summary>
    void OnEnable()
    {
        // General settings
        _range = serializedObject.FindProperty("range");
        _fireRate = serializedObject.FindProperty("fireRate");
        turretType = serializedObject.FindProperty("turretType");

        // Visuals and effects
        _bulletPrefab = serializedObject.FindProperty("_bulletPrefab");
        _fireEffect = serializedObject.FindProperty("_fireEffect");
        _laserBeam = serializedObject.FindProperty("_laserBeam");
        _impactLight = serializedObject.FindProperty("_impactLight");
        _impactEffect = serializedObject.FindProperty("_impactEffect");

        // Laser-specific settings
        _laserDamage = serializedObject.FindProperty("_laserDamage");
        _slowAmount = serializedObject.FindProperty("_slowAmount");

        // Sniper-specific settings
        _infiniteRange = serializedObject.FindProperty("_infiniteRange");
        _sniperDamage = serializedObject.FindProperty("_sniperDamage");

        // Rotation and Firing
        _rotationPivot = serializedObject.FindProperty("_rotationPivot");
        _firePoint = serializedObject.FindProperty("_firePoint");
        _turnSpeed = serializedObject.FindProperty("_turnSpeed");
    }

    /// <summary>
    /// Draws the custom inspector GUI for the Turret component.
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawGeneralSettings();
        DrawRotationSettings();
        DrawTurretTypeSettings();

        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Draws the general settings for the turret.
    /// </summary>
    private void DrawGeneralSettings()
    {
        EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_fireRate, new GUIContent("Fire Rate", "The rate of fire of the turret."));

        if ((TurretType)turretType.enumValueIndex != TurretType.Sniper)
        {
            EditorGUILayout.PropertyField(_range, new GUIContent("Range", "The range of the turret."));
        }
    }

    /// <summary>
    /// Draws the rotation and firing settings for the turret.
    /// </summary>
    private void DrawRotationSettings()
    {
        if ((TurretType)turretType.enumValueIndex != TurretType.Sniper)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Rotating and Firing", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_rotationPivot, new GUIContent("Rotation Pivot", "The pivot point for turret rotation."));
            EditorGUILayout.PropertyField(_firePoint, new GUIContent("Fire Point", "The point where projectiles are fired from."));
            EditorGUILayout.PropertyField(_turnSpeed, new GUIContent("Turn Speed", "The rotation speed of the turret."));
        }
    }

    /// <summary>
    /// Draws turret-type-specific settings for the turret.
    /// </summary>
    private void DrawTurretTypeSettings()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Turret Type", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(turretType, new GUIContent("Turret Type", "The type of the turret behavior (e.g., Standard, Cannon, Laser, Sniper)."));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Turret-Specific Settings", EditorStyles.boldLabel);

        switch ((TurretType)turretType.enumValueIndex)
        {
            case TurretType.Standard:
            case TurretType.Cannon:
                EditorGUILayout.PropertyField(_bulletPrefab, new GUIContent("Bullet Prefab", "The prefab for bullets fired by the turret."));
                EditorGUILayout.PropertyField(_fireEffect, new GUIContent("Fire Effect", "The visual effect played when the turret fires."));
                break;

            case TurretType.Laser:
                EditorGUILayout.PropertyField(_laserBeam, new GUIContent("Laser Beam", "The line renderer for the laser beam"));
                EditorGUILayout.PropertyField(_impactLight, new GUIContent("Impact Light", "The light effect at the laser impact point."));
                EditorGUILayout.PropertyField(_impactEffect, new GUIContent("Impact Effect", "The visual effect at the laser impact point."));
                EditorGUILayout.PropertyField(_laserDamage, new GUIContent("Laser Damage", "The damage dealt per second by the laser turret."));
                EditorGUILayout.PropertyField(_slowAmount, new GUIContent("Slow Amount", "The slow effect applied to targets by the laser."));
                break;

            case TurretType.Sniper:
                EditorGUILayout.PropertyField(_infiniteRange, new GUIContent("Infinite Range", "Whether the turret has infinite range"));
                EditorGUILayout.PropertyField(_sniperDamage, new GUIContent("Sniper Damage", "The damage dealt by the sniper turret."));
                break;
        }
    }
}