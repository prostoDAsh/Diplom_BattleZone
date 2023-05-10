using Signals;
using UnityEngine;

namespace Enemy
{
    public class EnemyDemon : EnemyBase
    {
        protected override void TakeDamage(int damage)
        {
            var incomingDamage = damage / config.Armor;
            currentHealth -= incomingDamage;

            base.TakeDamage(damage);
        }

        protected override void Rip()  // todo ????? 
        {
            signal.Fire(new EnemyDieSignal(config.KillRewards, this));
            base.Rip();
        }
        
        protected override void Move()
        {
            _isMove = false;
            _timeChangePoint = 0;
        }
    }
    
}