using System;
using ADS;
using Cysharp.Threading.Tasks;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;
using Zenject;
using UI.Buttons;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private int delayShowPanelFail = 2000;

        private InterstitialAdExample _interstitialAdExample;
        
        private FailPanel _failPanel;
        private MenuPanel _menuPanel;
        private WinPanel _winPanel;
        private GamePanel _gamePanel;
        private Shop _shopPanel;
        private PausePanel _pausePanel;
        private AchievementsPanel _achievementsPanel;
        

        private SignalBus _signal;
        private ISaveSystem _saveSystem;

        [Inject]
        public void Construct(SignalBus signalBus, MenuPanel menuPanel, GamePanel gamePanel, 
            FailPanel failPanel, WinPanel winPanel, InterstitialAdExample interstitialAdExample, Shop shopPanel, 
            PausePanel pausePanel, AchievementsPanel achievementsPanel, ISaveSystem saveSystem)
        {
            _signal = signalBus;
            _menuPanel = menuPanel;
            _gamePanel = gamePanel;
            _failPanel = failPanel;
            _winPanel = winPanel;
            _shopPanel = shopPanel;
            _pausePanel = pausePanel;
            _achievementsPanel = achievementsPanel;
            _saveSystem = saveSystem;

            _interstitialAdExample = interstitialAdExample;
        }

        private void Start()
        {
            ShowPanel(PanelType.Menu);
            
            _signal.Subscribe<StartingGameSignal>(OnStartedGame);
            _signal.Subscribe<PlayerRipSignal>(OnPlayerRip);
            _signal.Subscribe<PlayerEnteredNextLevelTriggerSignal>(OnPlayerWin);
            _signal.Subscribe<ReturnMenuSignal>(OnReturnMenu);
            _signal.Subscribe<NextLevelSignal>(OnStartedGame);
            _signal.Subscribe<RestartLevelSignal>(OnRestartLevel);
            _signal.Subscribe<ShopPanelSignal>(OnShopPanel);
            _signal.Subscribe<ExitSignal>(OnExitButton);
            _signal.Subscribe<StopCurrentGameSignal>(OnStopCurrentGame);
            _signal.Subscribe<PlayCurrentGameSignal>(OnPlayGame);
            _signal.Subscribe<ExitAchievementsSignal>(OnExitAchievementsButton);
            _signal.Subscribe<AchievementsPanelSignal>(OnAchievementsPanel);
            _signal.Subscribe<GameShopSignal>(OnGameShop);
            _signal.Subscribe<ReklamaSignal>(OnReklama);

        }
        
        private void OnDestroy()
        {
            _signal.Unsubscribe<StartingGameSignal>(OnStartedGame);
            _signal.Unsubscribe<PlayerRipSignal>(OnPlayerRip);
            _signal.Unsubscribe<PlayerEnteredNextLevelTriggerSignal>(OnPlayerWin);
            _signal.Unsubscribe<ReturnMenuSignal>(OnReturnMenu);
            _signal.Unsubscribe<NextLevelSignal>(OnStartedGame);
            _signal.Unsubscribe<RestartLevelSignal>(OnRestartLevel);
            _signal.Unsubscribe<ShopPanelSignal>(OnShopPanel);
            _signal.Unsubscribe<ExitSignal>(OnExitButton);
            _signal.Unsubscribe<StopCurrentGameSignal>(OnStopCurrentGame);
            _signal.Unsubscribe<PlayCurrentGameSignal>(OnPlayGame);
            _signal.Unsubscribe<ExitAchievementsSignal>(OnExitAchievementsButton);
            _signal.Unsubscribe<AchievementsPanelSignal>(OnAchievementsPanel);
            _signal.Unsubscribe<GameShopSignal>(OnGameShop);
            _signal.Unsubscribe<ReklamaSignal>(OnReklama);
        }
        
        private void OnReturnMenu()
        {
           ShowPanel(PanelType.Menu);
        }
        
        private void OnReklama()
        {
            _interstitialAdExample.ShowAd();
            _saveSystem.GameData.CountCoins += 5;
            _signal.Fire(new AddCoinSignal(_saveSystem.GameData.CountCoins));
        }
        
        private void OnRestartLevel()
        {
            Time.timeScale = 1;
            ShowPanel(PanelType.Game);
        }
        
        private void OnGameShop()
        {
            Time.timeScale = 0;
            _shopPanel.gameObject.SetActive(true);
        }
        
        private void OnPlayerWin()
        {
            ShowPanel(PanelType.Win);
        }

        private async void OnPlayerRip()
        {
            await UniTask.Delay(delayShowPanelFail);
            _interstitialAdExample.ShowAd();
            
            ShowPanel(PanelType.Fail);
        }

        private void OnStartedGame()
        {
            Time.timeScale = 1;
            ShowPanel(PanelType.Game);
        }
        private void OnShopPanel()
        {
            _shopPanel.gameObject.SetActive(true);
        }

        private void OnAchievementsPanel()
        {
            _achievementsPanel.gameObject.SetActive(true);
        }

        private void OnExitAchievementsButton()
        {
            ShowPanel(PanelType.Menu);
        }

        private void OnExitButton()
        {
            if (_gamePanel.gameObject.activeSelf)
                Time.timeScale = 1;

            _shopPanel.gameObject.SetActive(false);
        }

        private void OnStopCurrentGame()
        {
            Time.timeScale = 0;
            _pausePanel.gameObject.SetActive(true);
        }

        private void OnPlayGame()
        {
            Time.timeScale = 1;
            _pausePanel.gameObject.SetActive(false);
        }

        private void ShowPanel(PanelType panelType)
        {
            _menuPanel.gameObject.SetActive(panelType == PanelType.Menu);
            _gamePanel.gameObject.SetActive(panelType == PanelType.Game);
            _winPanel.gameObject.SetActive(panelType == PanelType.Win);
            _failPanel.gameObject.SetActive(panelType == PanelType.Fail);
            _shopPanel.gameObject.SetActive(panelType == PanelType.Shop);
            _pausePanel.gameObject.SetActive(panelType == PanelType.Pause);
            _achievementsPanel.gameObject.SetActive(panelType == PanelType.Achievements);
        }
    }
}