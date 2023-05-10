using UnityEngine;

namespace Rewards
{
    public class Heart : RewardBace
    {
        [Header("Bonus")]
        [SerializeField] private float health = 0.1f;
        public float Health => health;
    }
}