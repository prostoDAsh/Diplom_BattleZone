using Configs;
using Enemy;

namespace Signals
{
    public struct EnemyDieSignal
    {
        private readonly EnemyConfig.Rewards[]  _rewardses;
        private readonly EnemyBase _enemyBase;
        public EnemyConfig.Rewards[] Rewardses => _rewardses;
        public EnemyBase EnemyBase => _enemyBase;

        public EnemyDieSignal(EnemyConfig.Rewards[] rewardses, EnemyBase enemyBase)
        {
            _rewardses = rewardses;
            _enemyBase = enemyBase;
        }
    }
}