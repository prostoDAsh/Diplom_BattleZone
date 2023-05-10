using System;
using Configs;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;
using Zenject;

namespace Player
{
    public class ActivePlayerState : IState, IInitializable, IDisposable
    {
        private bool _isAttack = false;

        private readonly PlayerStateMachine _playerStateMachine;
        private readonly DiePlayerState _diePlayerState;
        
        private readonly SignalBus _signal;
        private readonly PlayerConfig _config;
        private readonly PlayerAnimator _animator;
        private readonly Player _player;
        private readonly Joystick _joystick;

        public ActivePlayerState(PlayerAnimator playerAnimator, Joystick joystick, 
            Player player, PlayerConfig playerConfig, PlayerStateMachine playerStateMachine, 
            SignalBus signalBus) 
        {
            _animator = playerAnimator;
            _joystick = joystick;
            _player = player;
            _config = playerConfig;
            _playerStateMachine = playerStateMachine;
       
            _signal = signalBus;
        }
        
        public void Initialize()
        {
            _signal.Subscribe<AllEnemiesDiedSignal>(OnAllEnemiesDied);
            _signal.Subscribe<PlayerEnteredNextLevelTriggerSignal>(OnPlayerWon);
            _signal.Subscribe<NextLevelSignal>(OnNextLevel);
            _signal.Subscribe<StartingGameSignal>(OnStartedGame);
        }
        
        public void Dispose()
        {
            _signal.Unsubscribe<AllEnemiesDiedSignal>(OnAllEnemiesDied);
            _signal.Unsubscribe<PlayerEnteredNextLevelTriggerSignal>(OnPlayerWon);
            _signal.Unsubscribe<NextLevelSignal>(OnNextLevel);
            _signal.Unsubscribe<StartingGameSignal>(OnStartedGame);
        }
        
        public void Enter()
        {
            _animator.Move(1);
        }

        public void Exit()
        {
            _animator.Move(0);
        }

        public void Update()
        {
            if (_joystick.Direction != Vector2.zero)
            {
                var direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
                _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation,
                    Quaternion.LookRotation(direction),
                    _config.RotateSpeed * Time.deltaTime);

                var magnitude = _joystick.Direction.magnitude;
                _player.transform.position += _player.transform.forward * (_config.Speed * Time.deltaTime * magnitude);
                _animator.Move(magnitude);
                
                return;
            }
            
            _animator.Move(0);
            
            if(_isAttack)
                _playerStateMachine.ChangeState(PlayerStateType.Attack);
        }
        
        private void OnPlayerWon()
        {
            _joystick.OnPointerUp(null);
        }

        
        private void OnAllEnemiesDied()
        {
            _isAttack = false;
        }
        
        private void OnNextLevel()
        {
            _isAttack = true;
        }
        
        private void OnStartedGame()
        {
            _isAttack = true;
        }
    }
}