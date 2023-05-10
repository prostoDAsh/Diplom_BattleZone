using Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Level;
using UI;
using UnityEngine;
using Weapon;
using Zenject;

namespace Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected EnemyConfig config;
        
        protected float currentHealth;
        
        protected HealthBar healthBar;
        protected WeaponBase weapon;
        private ParticleSystem _particle;
        protected EnemyAnimator animator;
        
        protected Player.Player player;
        protected SignalBus signal;

        private bool _isActive;
        private float _counter;
        
        protected float _timeChangePoint;
        protected bool _isMove = true;
        
        protected Vector3 _direction;
        protected Vector3 _spawnPoint;
        protected Vector3 _movePoint;
        public bool IsActive => _isActive;
        
        [Inject]
        public void Construct(Player.Player player, SignalBus signal)
        {
            this.player = player;
            this.signal = signal;
        }

        protected virtual void Awake()
        {
            _particle = GetComponentInChildren<ParticleSystem>();
            animator = GetComponent<EnemyAnimator>();
            healthBar = GetComponentInChildren<HealthBar>();
            weapon = GetComponentInChildren<WeaponBase>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Bullet bullet) || !_isActive) return;
            
            if(bullet.Type == BulletType.Player)
                TakeDamage(bullet.Damage);
        }

        private void Update()
        {
            if(!_isActive || !player.IsActive) return;
            
            _timeChangePoint += Time.deltaTime;
            if (_timeChangePoint > 10f && _isMove) // todo Мэджик 10
            {
                Move();
                return;
            }

            FollowTarget();

            _counter += Time.deltaTime;
            if (!(_counter >= config.DelayBetweenAttacks)) return;
            
            Attack();
            _counter = 0;

        }

        public void Init(PointEnemy point)
        {
            var position = point.TwoPoint.position;
            
            _spawnPoint = position;
            _movePoint = position;
            transform.position = point.transform.position;
            
            currentHealth = 1f;
            _isActive = true;
            
            healthBar.Show(currentHealth).Forget();
        }

        protected virtual void TakeDamage(int damage)
        {
            healthBar.Show(currentHealth).Forget();
            
            if (currentHealth <= 0)
                Rip();
        }

        protected virtual async void Rip()
        {
            _isActive = false;
            _particle.Play();
            
            animator.Rip();
            await UniTask.Delay(1000);
            await transform.DOMoveY(-0.3f, config.TimeDieEnemy); // todo мЭжик -0.3f
            
            Destroy(gameObject);
        }

        private void FollowTarget()
        {
            _direction = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), 
                config.RotateSpeed * Time.deltaTime);
        }

        protected virtual void Attack()
        {
            animator.Attack();
            weapon.Fire(player.transform.position, BulletType.Enemy);
        }

        protected abstract void Move();
        
        

    }
}