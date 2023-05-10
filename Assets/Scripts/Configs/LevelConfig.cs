using Enums;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int timeSpawnBetweenEnemyMS = 500;
        [SerializeField] private EnemyType[] enemyTypes;
        
        public int TimeSpawnBetweenEnemyMS => timeSpawnBetweenEnemyMS;
        public EnemyType[] EnemyTypes => enemyTypes;
    }
}