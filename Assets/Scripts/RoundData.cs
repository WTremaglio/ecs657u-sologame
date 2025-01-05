using UnityEngine;

/// <summary>
/// Provides data for all rounds in the game.
/// </summary>
public class RoundData : MonoBehaviour
{
    /// <summary>
    /// Returns the configuration for all rounds in the game.
    /// </summary>
    /// <returns>An array of rounds with enemy spawn configurations.</returns>
    public static Round[] GetRounds()
    {
        return new Round[]
        {
            // Example / Debug Round
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 5, delayBetween = 1.0f },
                    new() { enemyType = EnemyType.Blue, count = 5, delayBetween = 0.8f },
                    new() { enemyType = EnemyType.Green, count = 5, delayBetween = 0.4f },
                    new() { enemyType = EnemyType.Yellow, count = 5, delayBetween = 0.2f },
                }
            },
            // Round 1
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 12, delayBetween = 1.0f },
                }
            },
            // Round 2
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 25, delayBetween = 1.0f },
                }
            },
            // Round 3
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 24, delayBetween = 1.0f },
                    new() { enemyType = EnemyType.Blue, count = 5, delayBetween = 0.8f },
                }
            },
            // Round 4
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 10, delayBetween = 1.0f },
                    new() { enemyType = EnemyType.Blue, count = 24, delayBetween = 0.8f },
                }
            },
            // Round 5
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 30, delayBetween = 1.0f },
                    new() { enemyType = EnemyType.Blue, count = 25, delayBetween = 0.8f },
                }
            },
            // Round 6
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Green, count = 15, delayBetween = 0.4f },
                }
            },
            // Round 7
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Blue, count = 75, delayBetween = 0.8f },
                }
            },
            // Round 8
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 115, delayBetween = 1.0f },
                    new() { enemyType = EnemyType.Blue, count = 68, delayBetween = 0.8f },
                }
            },
            // Round 9
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 49, delayBetween = 1.0f },
                    new() { enemyType = EnemyType.Green, count = 22, delayBetween = 0.4f },
                }
            },
            // Round 10
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Green, count = 40, delayBetween = 0.4f },
                }
            },
            // Round 11
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Yellow, count = 24, delayBetween = 0.2f },
                }
            },
            // Round 12
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Blue, count = 30, delayBetween = 0.8f },
                    new() { enemyType = EnemyType.Green, count = 25, delayBetween = 0.4f },
                    new() { enemyType = EnemyType.Yellow, count = 3, delayBetween = 0.2f },
                }
            },
            // Round 13
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 40, delayBetween = 0.8f },
                    new() { enemyType = EnemyType.Blue, count = 75, delayBetween = 0.6f },
                    new() { enemyType = EnemyType.Green, count = 30, delayBetween = 0.4f },
                }
            },
            // Round 14
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Yellow, count = 26, delayBetween = 0.2f },
                }
            },
            // Round 15
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Red, count = 30, delayBetween = 0.8f },
                    new() { enemyType = EnemyType.Green, count = 60, delayBetween = 0.4f },
                }
            },
            // Round 16
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Blue, count = 80, delayBetween = 0.6f },
                    new() { enemyType = EnemyType.Green, count = 80, delayBetween = 0.4f },
                }
            },
            // Round 17
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Blue, count = 150, delayBetween = 0.6f },
                    new() { enemyType = EnemyType.Green, count = 30, delayBetween = 0.4f },
                }
            },
            // Round 18
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Blue, count = 30, delayBetween = 0.6f },
                    new() { enemyType = EnemyType.Green, count = 26, delayBetween = 0.4f },
                    new() { enemyType = EnemyType.Yellow, count = 28, delayBetween = 0.2f },
                }
            },
            // Round 19
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Green, count = 92, delayBetween = 0.4f },
                }
            },
            // Round 20
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Blue, count = 40, delayBetween = 0.6f },
                    new() { enemyType = EnemyType.Yellow, count = 60, delayBetween = 0.2f },
                }
            },
            // Round 21
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Blue, count = 10, delayBetween = 0.6f },
                    new() { enemyType = EnemyType.Green, count = 85, delayBetween = 0.4f },
                    new() { enemyType = EnemyType.Yellow, count = 30, delayBetween = 0.2f },
                }
            },
            // Round 22
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Yellow, count = 45, delayBetween = 0.2f },
                }
            },
            // Round 23
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Green, count = 35, delayBetween = 0.4f },
                    new() { enemyType = EnemyType.Yellow, count = 64, delayBetween = 0.2f },
                }
            },
            // Round 24
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Blue, count = 20, delayBetween = 0.6f },
                    new() { enemyType = EnemyType.Green, count = 60, delayBetween = 0.4f },
                    new() { enemyType = EnemyType.Yellow, count = 30, delayBetween = 0.2f },
                }
            },
            // Round 25
            new() {
                enemies = new Round.SpawnInfo[]
                {
                    new() { enemyType = EnemyType.Green, count = 80, delayBetween = 0.4f },
                    new() { enemyType = EnemyType.Yellow, count = 50, delayBetween = 0.2f },
                }
            },
        };
    }
}