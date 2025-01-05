using System;

/// <summary>
/// Represents a round in the game.
/// </summary>
[Serializable]
public class Round
{
    /// <summary>
    /// Configuration for a specific enemy spawn during a round.
    /// </summary>
    [Serializable]
    public class SpawnInfo
    {
        /// <summary>
        /// The type of enemy to spawn.
        /// </summary>
        public EnemyType enemyType;

        /// <summary>
        /// The number of enemies to spawn.
        /// </summary>
        public int count;

        /// <summary>
        /// Delay between spawns of this enemy type.
        /// </summary>
        public float delayBetween;
    }

    /// <summary>
    /// List of enemy spawn configuration for this round.
    /// </summary>
    public SpawnInfo[] enemies;  // Array of spawn configurations
}