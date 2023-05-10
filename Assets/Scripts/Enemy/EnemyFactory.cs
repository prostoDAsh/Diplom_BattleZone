using Enums;
using Interfaces;
using Zenject;

namespace Enemy
{
    public class EnemyFactory: IEnemyFactory
    {
        private readonly EnemyEasy _enemyEasy;
        private readonly EnemyHard _enemyHard;
        private readonly EnemyDemon _enemyDemon;
        private readonly DiContainer _diContainer;

        public EnemyFactory(EnemyEasy enemyEasy, EnemyHard enemyHard, EnemyDemon enemyDemon, DiContainer diContainer)
        {
            _enemyEasy = enemyEasy;
            _enemyHard = enemyHard;
            _enemyDemon = enemyDemon;
            _diContainer = diContainer;
        }


        public EnemyBase CreateEnemy(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Easy:
                    var enemyE = _diContainer.InstantiatePrefab(_enemyEasy).GetComponent<EnemyEasy>();
                    return enemyE;
                
                case EnemyType.Hard:
                    var enemyH = _diContainer.InstantiatePrefab(_enemyHard).GetComponent<EnemyHard>();
                    return enemyH;
                
                case EnemyType.Demon:
                    var enemyD = _diContainer.InstantiatePrefab(_enemyDemon).GetComponent<EnemyDemon>();
                    return enemyD;
                
                default:
                    return null;
            }
        }
    }
}