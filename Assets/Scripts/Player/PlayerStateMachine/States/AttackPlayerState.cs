using System.Collections.Generic;
using System.Linq;
using Configs;
using Enums;
using Interfaces;
using UnityEngine;
using Weapon;
using Zenject;

namespace Player
{
    public class AttackPlayerState : IState
    {
        private float _counterTime;
        private bool _isAttack;
        
        private List<GameObject> _enemies = new List<GameObject>(10); 
        private GameObject _currentTarget;
        
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly Joystick _joystick;
        private readonly PlayerAnimator _animator;
        private readonly WeaponBase _weapon;
        private readonly PlayerConfig _config;
        private readonly Player _player;
        private readonly DiContainer _container;

        public AttackPlayerState(PlayerStateMachine playerStateMachine, Joystick joystick, 
            PlayerAnimator animator, WeaponBase weapon, PlayerConfig config, Player player, 
            DiContainer container)
        {
            _playerStateMachine = playerStateMachine;
            _joystick = joystick;
            _animator = animator;
            _weapon = weapon;
            _config = config;
            _player = player;
            _container = container;
        }
        

        public void Enter()
        {
            if (_enemies.Count == 0)
                _enemies = _container.Resolve<Level.Level>().Enemies;
            
            _isAttack = true;
        }

        public void Exit()
        {
          
        }

        public void Update()
        {
            if (_joystick.Direction != Vector2.zero)
            {
                _playerStateMachine.ChangeState(PlayerStateType.Active);
                return;
            }
            
            if(!_isAttack) return;
            
            SearchNearestEnemy();
            _counterTime += Time.deltaTime;
            
            if (!(_config.DelayBetweenAttacks <= _counterTime) || !_currentTarget) return;
            _counterTime = 0;
            Attack();
        }
        
        private void SearchNearestEnemy()
        {
            _currentTarget = _enemies.OrderBy(enemy => Vector3.SqrMagnitude(enemy.transform.position - _player.transform.position)).
                FirstOrDefault();

            if (_currentTarget == null)
            {
                _isAttack = false;
                return;
            }

            var direction = (_currentTarget.transform.position - _player.transform.position).normalized;
            _player.transform.rotation = Quaternion.LookRotation(direction);
        }
        
        private void Attack()
        {
            if(!_currentTarget.activeSelf) return;
            
            _weapon.Fire(_currentTarget.transform.position, BulletType.Player);
            _animator.Attack();
        }
    }
}