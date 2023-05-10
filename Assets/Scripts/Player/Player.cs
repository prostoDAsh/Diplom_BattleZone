using Cinemachine;
using Configs;
using Interfaces;
using Level;
using Signals;
using Rewards;
using UI;
using UnityEngine;
using Weapon;
using Zenject;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private bool _isActive = false;
        
        private float _currentHealth = 1;
        private int _deathsCount = 0;

        private Vector3 _startPosition;
        
        public bool IsActive => _isActive;

        private HealthBar _healthBar;
        private PlayerConfig _config;
        private SignalBus _signal;
        private CinemachineVirtualCamera _camera;
        private ISaveSystem _saveSystem;
        private PlayerAnimator _animator;

        [Inject]
        public void Construct(HealthBar healthBar, PlayerConfig config, 
            SignalBus signalBus, CinemachineVirtualCamera virtualCamera, 
            ISaveSystem saveSystem, PlayerAnimator animator)
        {
            _healthBar = healthBar;
            _config = config;
            _signal = signalBus;
            _camera = virtualCamera;
            _saveSystem = saveSystem;
            _animator = animator;
        }

        private void Start()
        {
            _config.Init();
            _startPosition = transform.position;
            
            _signal.Subscribe<StartingGameSignal>(OnStartedGame);
            _signal.Subscribe<NextLevelSignal>(OnNextLevel);
            _signal.Subscribe<RestartLevelSignal>(OnRestartLevel);
            _signal.Subscribe<ReturnMenuSignal>(OnReturnMenu);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Bullet bullet) && _isActive)
            {
                TakeDamage(bullet.Damage);
            }

            if (other.gameObject.TryGetComponent(out RewardBace component))
            {
                component.gameObject.SetActive(false);
                
                switch (component)
                {
                    case Coin:
                        _signal.Fire(new AddCoinSignal(++_saveSystem.GameData.CountCoins));
                        break;
                    
                    case Heart heart:
                        if (_currentHealth < 1)
                        {
                            _currentHealth += heart.Health;
                            _healthBar.Show(_currentHealth).Forget();
                        }
                        break;
                    
                    case Star:
                        _signal.Fire(new AddStarSignal(++_saveSystem.GameData.CountStars));
                        break;
                }
            }

            if (other.gameObject.GetComponent<NextLevelTrigger>())
            {
                _signal.Fire<PlayerEnteredNextLevelTriggerSignal>();
                _isActive = false;
            }
        }

        private void OnDestroy()
        {
            _signal.Unsubscribe<StartingGameSignal>(OnStartedGame);
            _signal.Unsubscribe<NextLevelSignal>(OnNextLevel);
            _signal.Unsubscribe<RestartLevelSignal>(OnRestartLevel);
            _signal.Unsubscribe<ReturnMenuSignal>(OnReturnMenu);
        }

        private void OnStartedGame()
        {
            PutTheCamera();
            
             _healthBar.Show(_currentHealth).Forget();
            _isActive = true;
        }

        private void OnNextLevel()
        {
            _isActive = true;
            _healthBar.Show(_currentHealth).Forget();
        }
        
        
        private void OnRestartLevel()
        {
            _isActive = true;
            _currentHealth = 1;

            _healthBar.Show(_currentHealth).Forget();
        }
        
        private void OnReturnMenu()
        {
            _isActive = true;
            _currentHealth = 1;

            transform.position = _startPosition;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            
            _animator.Idle();
        }


        private void PutTheCamera()
        {
            _camera.m_Follow = transform;
            _camera.m_LookAt = transform;
        }

        private void TakeDamage(int damage)
        {
            var incomingDamage = damage / _config.Armor;
            _currentHealth -= incomingDamage;
            
            _healthBar.Show(_currentHealth).Forget();
            
            if (_currentHealth > 0) return;
            
            _isActive = false;
            _deathsCount++;
            _signal.Fire<PlayerRipSignal>();
        }
        
    }
}
