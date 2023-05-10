using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Interfaces;
using Level;
using Zenject;

namespace Player
{
    public class StartPlayerState : IState
    {
        private StartPlayerPoint _startPoint = null;
        
        private readonly PlayerStateMachine _stateMachine;
        private readonly ActivePlayerState _activePlayerState;
        
        private readonly Player _player;
        private readonly DiContainer _diContainer;

        public StartPlayerState(PlayerStateMachine stateMachine,  
             Player player, DiContainer diContainer)
        {
            _stateMachine = stateMachine;
            _player = player;
            _diContainer = diContainer;
        }
        
        public void Enter()
        {
            _startPoint = _diContainer.Resolve<Level.Level>().PlayerPoint;
            GetStartPoint().Forget();
        }

        public void Exit()
        {
           
        }

        public void Update()
        {
      
        }

        private async UniTaskVoid GetStartPoint()
        {
            await _player.transform.DOMove(_startPoint.transform.position, 0.5f); // todo мЭджик 0.5f
            _stateMachine.ChangeState(PlayerStateType.Active);
        }
    }
}