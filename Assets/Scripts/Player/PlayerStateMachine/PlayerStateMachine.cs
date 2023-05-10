using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using Signals;
using Zenject;

namespace Player
{
    public class PlayerStateMachine :  ITickable, IInitializable, IDisposable
    {
        private bool _isTick = false;
        
        private IState _currentState;
        private List<IState> _states;

        private SignalBus _signal;

        [Inject]
        public void Construct (ActivePlayerState active, AttackPlayerState attack, 
            DiePlayerState die, StartPlayerState start, SignalBus signalBus)
        {
            _states = new List<IState> {active, attack, die, start};
            _signal = signalBus;
        }
        
        public void Initialize()
        {
            _signal.Subscribe<PlayerRipSignal>(OnPlayerRip);
            _signal.Subscribe<StartingGameSignal>(OnStartedGame);
            _signal.Subscribe<NextLevelSignal>(OnNextLevel);
            
            _signal.Subscribe<RestartLevelSignal>(OnRestartLevel);
        }
        
        public void Dispose()
        {
            _signal.Unsubscribe<PlayerRipSignal>(OnPlayerRip);
            _signal.Unsubscribe<StartingGameSignal>(OnStartedGame);
            _signal.Unsubscribe<NextLevelSignal>(OnNextLevel);
            
            _signal.Unsubscribe<RestartLevelSignal>(OnRestartLevel);
        }

        public void Tick()
        {
            if(!_isTick) return;
            _currentState.Update();
        }

        public void ChangeState(PlayerStateType nextStateEnum)
        {
            _currentState?.Exit();
            _currentState = _states[(int)nextStateEnum];
            _currentState.Enter();
        }
       
        private void OnPlayerRip()
        {
            _isTick = false;
            ChangeState(PlayerStateType.Die);
        }
        private void OnStartedGame()
        {
            ChangeState(PlayerStateType.None);
            _isTick = true;
        }
        
        private void OnRestartLevel()
        {
            _isTick = true;
            ChangeState(PlayerStateType.Active);
        }
        
        private void OnNextLevel()
        {
            ChangeState(PlayerStateType.Active);
            _isTick = true;
        }
    }

}