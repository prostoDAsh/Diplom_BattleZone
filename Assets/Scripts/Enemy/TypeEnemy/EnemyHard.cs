using Signals;
using UnityEngine;

namespace Enemy
{
    public class EnemyHard : EnemyBase
    {
        protected override void TakeDamage(int damage)
        {
            var incomingDamage = damage / config.Armor;
            currentHealth -= incomingDamage;

            base.TakeDamage(damage);
        }

        protected override void Rip()  
        {
            signal.Fire(new EnemyDieSignal(config.KillRewards, this));
            base.Rip();
        }
        
        protected override void Move()
        {
            transform.rotation = Quaternion.LookRotation(_movePoint.normalized);
            
            transform.position = Vector3.MoveTowards(transform.position, _movePoint, config.Speed * Time.deltaTime);
            animator.Move(1f);

            if (transform.position != _movePoint) return;
            animator.Move(0f);
            
            _timeChangePoint = 0;
            _isMove = false;
        }
    }
}