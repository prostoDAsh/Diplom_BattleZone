using UnityEngine;

namespace Player
{
    public class PlayerAnimator 
    {
        private readonly Animator _animator;
        
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Idle1 = Animator.StringToHash("Idle");

        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void Idle()
        {
            _animator.SetTrigger(Idle1);
        }

        public void Rip()
        {
            _animator.SetTrigger(Death);
        }
        
        public void Attack()
        {
            _animator.SetTrigger(Attack1);
        }

        public void Move(float speed)
        {
            _animator.SetFloat(Walk, speed);
        }
    }
}