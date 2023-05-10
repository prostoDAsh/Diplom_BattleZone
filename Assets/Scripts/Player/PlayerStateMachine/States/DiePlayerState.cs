using Interfaces;

namespace Player
{
    public class DiePlayerState : IState
    {
        private readonly PlayerAnimator _animator;
        
        public DiePlayerState(PlayerAnimator playerAnimator)
        {
            _animator = playerAnimator;
        }
        
        public void Enter()
        {
            _animator.Rip();
        }

        public void Exit()
        { }

        public void Update()
        { }
        
        
    }
}