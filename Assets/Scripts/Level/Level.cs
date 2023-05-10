using System.Collections.Generic;
using Configs;
using Cysharp.Threading.Tasks;
using Interfaces;
using Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private LevelConfig config;
        
        private StartPlayerPoint _startPlayerPoint;
        private PointEnemy[] _pointsEnemy;
        private NextLevelTrigger _nextLevelTrigger;
        
        private IEnemyFactory _factory;
        private SignalBus _signal;

        private List<GameObject> _enemies = new List<GameObject>(10);
        private List<GameObject> _rewards = new List<GameObject>(10);
        private Player.Player _player;

        public List<GameObject> Enemies => _enemies;
        public StartPlayerPoint PlayerPoint => _startPlayerPoint;

        [Inject]
        public void Construct(SignalBus signalBus, IEnemyFactory factory, Player.Player player)
        {
            _signal = signalBus;
            _factory = factory;
            _player = player;
        }

        private void Awake()
        {
            _startPlayerPoint = GetComponentInChildren<StartPlayerPoint>();
            _pointsEnemy = GetComponentsInChildren<PointEnemy>();
            
            _nextLevelTrigger = GetComponentInChildren<NextLevelTrigger>();
            _nextLevelTrigger.gameObject.SetActive(false);
        }

        private void Start()
        {
            _signal.Subscribe<EnemyDieSignal>(OnEnemyDied);
            _signal.Subscribe<RestartLevelSignal>(OnRestartLevel);
            _signal.Subscribe<ReturnMenuSignal>(OnReturnMenu);
            
            SpawnEnemy();
        }
        

        private void OnDestroy() 
        {
            _signal.Unsubscribe<EnemyDieSignal>(OnEnemyDied);
            _signal.Unsubscribe<RestartLevelSignal>(OnRestartLevel);
            _signal.Unsubscribe<ReturnMenuSignal>(OnReturnMenu);
        }
        
        private void OnEnemyDied(EnemyDieSignal dieSignal) 
        {
            _enemies.Remove(dieSignal.EnemyBase.gameObject);
            SetRewards(dieSignal.Rewardses, dieSignal.EnemyBase.transform.position);
            
            if(_enemies.Count == 0 && _player.IsActive) 
                PlayerWon();
        }
        
        private void OnRestartLevel()
        {
            foreach (var enemy in _enemies)
                Destroy(enemy);
            
            _enemies.Clear();
         
            SpawnEnemy();
        }
        
        private void OnReturnMenu()
        {
            foreach (var enemy in _enemies)
                Destroy(enemy);
            
            _enemies.Clear();
        }
        
        private void SetRewards(EnemyConfig.Rewards[] rewards, Vector3 pos) 
        {
            var point = new Vector3(pos.x, 0, pos.z);

            foreach (var reward in rewards)
            {
                for (int i = 0; i < reward.count; i++)
                {
                   var item = Instantiate(reward.prefab, point, reward.prefab.transform.rotation, transform);
                   _rewards.Add(item.gameObject);
                }
            }
        }
        

        private async void SpawnEnemy()
        {
            foreach (var point in _pointsEnemy)
            {
                await UniTask.Delay(config.TimeSpawnBetweenEnemyMS);
                var random = Random.Range(0, config.EnemyTypes.Length);
                var enemy = _factory.CreateEnemy(config.EnemyTypes[random]);
                
                enemy.Init(point);
                _enemies.Add(enemy.gameObject);
            }
        }

        private void PlayerWon()
        {
            _signal.Fire<AllEnemiesDiedSignal>();
            _nextLevelTrigger.gameObject.SetActive(true);
            _nextLevelTrigger.InitParticle();
        }
        
        
    }
}