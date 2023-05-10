using System;
using Interfaces;
using Signals;
using Zenject;
using Object = UnityEngine.Object;


public class GameManager : IInitializable, IDisposable
    {
        private Level.Level _currentLevel;

        private readonly SignalBus _signal;
        private readonly GameConfigInstaller.LevelsPrefab _levels;
        private readonly DiContainer _diContainer;
        private readonly Player.Player _player;
        private readonly ISaveSystem _saveSystem;

        public GameManager(SignalBus signal, GameConfigInstaller.LevelsPrefab levels,
            DiContainer diContainer, Player.Player player, ISaveSystem saveSystem)
        {
            _signal = signal;
            _levels = levels;
            _diContainer = diContainer;
            _player = player;
            _saveSystem = saveSystem;
        }
        
        public void Initialize()
        {
            _saveSystem.Initialize();
            
           // Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);

            _signal.Subscribe<StartingGameSignal>(OnStartedGame);
            _signal.Subscribe<PlayerEnteredNextLevelTriggerSignal>(OnPlayerWined);
            _signal.Subscribe<NextLevelSignal>(OnNextLevel);
            _signal.Subscribe<RestartLevelSignal>(OnRestartLevel);
        }
        

        public void Dispose()
        {
            _signal.Unsubscribe<StartingGameSignal>(OnStartedGame);
            _signal.Unsubscribe<PlayerEnteredNextLevelTriggerSignal>(OnPlayerWined);
            _signal.Unsubscribe<NextLevelSignal>(OnNextLevel);
            _signal.Unsubscribe<RestartLevelSignal>(OnRestartLevel);

            _saveSystem.SaveData();
        }

        private void OnNextLevel()
        {
            _player.transform.position = _currentLevel.PlayerPoint.transform.position; 
            _currentLevel.PlayerPoint.PlayParticle();
        }
        
        private void OnRestartLevel()
        {
            _player.transform.position = _currentLevel.PlayerPoint.transform.position;
        }
        
        private void OnStartedGame()
        { 
            SetLevel();
            _currentLevel.PlayerPoint.PlayParticle();
        }
        
        private void OnPlayerWined()
        {
            _saveSystem.GameData.CurrentLevel++;
            _signal.Fire(new CurrentLevelSignal(_saveSystem.GameData.CurrentLevel));
            SetLevel();
        }
        
        private void SetLevel()
        {
            if (_saveSystem.GameData.CurrentLevel >= _levels.Prefabs.Length)
                _saveSystem.GameData.CurrentLevel %= _levels.Prefabs.Length;

            if (_currentLevel != null)
            {
                Object.Destroy(_currentLevel.gameObject);
                _diContainer.Unbind<Level.Level>();
            }

            _currentLevel = _diContainer.InstantiatePrefab(_levels.Prefabs[_saveSystem.GameData.CurrentLevel]).GetComponent<Level.Level>();
            _diContainer.Bind<Level.Level>().FromInstance(_currentLevel).NonLazy();
        }
        
    }
