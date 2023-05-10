using Enemy;
using Enums;

namespace Interfaces
{
    public interface IEnemyFactory
    {
        public EnemyBase CreateEnemy(EnemyType enemyType);
    }
}