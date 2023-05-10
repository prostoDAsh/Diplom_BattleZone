using Interfaces;
using Signals;
using TMPro;
using Zenject;

namespace UI
{
    public class GamePanel : BasePanel
    {
        private TextMeshProUGUI _coin;
        private TextMeshProUGUI _star;
        private TextMeshProUGUI _currentLevel;
        
        private ISaveSystem _saveSystem;

        [Inject]
        public void Construct(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        private void Awake()
        {
            _coin = GetComponentInChildren<CoinText>().GetComponent<TextMeshProUGUI>();
            _star = GetComponentInChildren<StarText>().GetComponent<TextMeshProUGUI>();
            _currentLevel = GetComponentInChildren<CurrentLevelText>().GetComponent<TextMeshProUGUI>();
            
        }

        private void OnEnable()
        {
            _coin.text = _saveSystem.GameData.CountCoins.ToString();
            _star.text = _saveSystem.GameData.CountStars.ToString();
            _currentLevel.text = _saveSystem.GameData.CurrentLevel.ToString();
        }

        private void Start()
        {
            signal.Subscribe<AddCoinSignal>(OnAddCoin);
            signal.Subscribe<CurrentLevelSignal>(OnChangeLevel);
            signal.Subscribe<AddStarSignal>(OnAddStar);
            signal.Subscribe<ExitSignal>(OnExitButton);
        }
        
        private void OnDestroy()
        {
            signal.Unsubscribe<AddCoinSignal>(OnAddCoin);
            signal.Unsubscribe<CurrentLevelSignal>(OnChangeLevel);
            signal.Unsubscribe<AddStarSignal>(OnAddStar);
            signal.Unsubscribe<ExitSignal>(OnExitButton);
        }
        private void OnAddCoin(AddCoinSignal coinSignal)
        {
            _coin.text = coinSignal.Coin.ToString();
        }
        private void OnAddStar(AddStarSignal starSignal)
        {
            _star.text = starSignal.CountStars.ToString();
        }
        private void OnChangeLevel(CurrentLevelSignal index)
        {
            _currentLevel.text = index.IndexLevel.ToString();
        }
        
        private void OnExitButton()
        {
            _coin.text = _saveSystem.GameData.CountCoins.ToString();
        }
        
    }
}