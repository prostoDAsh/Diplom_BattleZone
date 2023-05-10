using System;
using Rewards;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Time")]
        [SerializeField] private float delayBetweenAttacks = 2f;
        [SerializeField] private float timeDieEnemy = 3f;
        
        [Header("Settings Enemy")]
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private float armor = 20f;
        [SerializeField] private float speed = 1.5f;
        
        [Header("KillRewards")]
        [SerializeField] private Rewards[] killRewards;

        
        public float DelayBetweenAttacks => delayBetweenAttacks;
        public float Armor => armor;
        public float Speed => speed;
        public float TimeDieEnemy => timeDieEnemy;
        public Rewards[] KillRewards => killRewards;
        public float RotateSpeed => rotateSpeed;

        [Serializable]
        public class Rewards
        {
            public RewardBace prefab;
            public int count;
        }
    }
}