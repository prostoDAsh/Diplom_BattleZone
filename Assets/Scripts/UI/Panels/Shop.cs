using Configs;
using Interfaces;
using Signals;
using TMPro;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class Shop : MonoBehaviour
    {
        private ISaveSystem _saveSystem;
        private PlayerConfig _playerConfig;
        
        private const float UpgradeSpeed = 0.3f;
        private const float UpgradeArmor = 1.5f;
        private const float UpgradeWeapon = 0.1f;
        
        private Button _upgradeSpeedButton;
        private Button _upgradeArmorButton;
        private Button _upgradeWeaponButton;
        
        private SignalBus _signalBus;
        private TextMeshProUGUI _coin;
        private TextMeshProUGUI _star;

        private TextMeshProUGUI _armorCost;
        private TextMeshProUGUI _weaponCost;
        private TextMeshProUGUI _speedCost;
    

        [Inject] 
        public void Construct(ISaveSystem saveSystem, PlayerConfig playerConfig, SignalBus signalBus)
        {
            _saveSystem = saveSystem;
            _playerConfig = playerConfig;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _coin = GetComponentInChildren<CoinText>().GetComponent<TextMeshProUGUI>();
            _star = GetComponentInChildren<StarText>().GetComponent<TextMeshProUGUI>();

            _armorCost = GetComponentInChildren<ArmorCostTxt>().GetComponent<TextMeshProUGUI>();
            _weaponCost = GetComponentInChildren<WeaponCostTxt>().GetComponent<TextMeshProUGUI>();
            _speedCost = GetComponentInChildren<SpeedCostTxt>().GetComponent<TextMeshProUGUI>();
            
            
            _upgradeSpeedButton = GetComponentInChildren<UpgradeSpeedButton>().GetComponent<Button>();
            _upgradeArmorButton = GetComponentInChildren<UpgradeArmorButton>().GetComponent<Button>();
            _upgradeWeaponButton = GetComponentInChildren<UpgradeWeaponButton>().GetComponent<Button>();
        }

        private void OnEnable()
        {
            CoinShow();
            StarShow();
            CheckedButton();
            CostsShow();
        }

        private void Start()
        {
            _signalBus.Subscribe<UpgradeSpeedSignal>(OnUpgradeSpeed);
            _signalBus.Subscribe<UpgradeArmorSignal>(OnUpgradeArmor);
            _signalBus.Subscribe<UpgradeWeaponSignal>(OnUpgradeWeapon);
        }
        

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<UpgradeSpeedSignal>(OnUpgradeSpeed);
            _signalBus.Unsubscribe<UpgradeArmorSignal>(OnUpgradeArmor);
            _signalBus.Unsubscribe<UpgradeWeaponSignal>(OnUpgradeWeapon);
        }

        private void OnUpgradeSpeed()
        {
            _saveSystem.GameData.CountCoins -= _saveSystem.GameData.UpgradeSpeedCost;
            _playerConfig.AddSpeed(UpgradeSpeed);
            _saveSystem.GameData.UpgradeSpeedCost += _saveSystem.GameData.UpgradeSpeedCost; 
            
            CostsShow();
            CheckedButton();
            CoinShow();
        }

        private void OnUpgradeArmor()
        {
            _saveSystem.GameData.CountCoins -= _saveSystem.GameData.UpgradeArmorCost;
            _playerConfig.AddArmor(UpgradeArmor);
            _saveSystem.GameData.UpgradeArmorCost += _saveSystem.GameData.UpgradeArmorCost; 
            
            CostsShow();
            CheckedButton();
            CoinShow();
        }
        
        private void OnUpgradeWeapon()
        {
            _saveSystem.GameData.CountCoins -= _saveSystem.GameData.UpgradeWeaponCost;
            _playerConfig.ReductionDelayBetweenAttacks(UpgradeWeapon);
            _saveSystem.GameData.UpgradeWeaponCost += _saveSystem.GameData.UpgradeWeaponCost;

            CostsShow();
            CheckedButton();
            CoinShow();
        }

        private void CoinShow()
        {
            _coin.text = _saveSystem.GameData.CountCoins.ToString();
        }

        private void StarShow()
        {
            _star.text = _saveSystem.GameData.CountStars.ToString();
        }

        private void CostsShow()
        {
            _armorCost.text = $"Cost: {_saveSystem.GameData.UpgradeArmorCost}";
            _weaponCost.text = $"Cost: {_saveSystem.GameData.UpgradeWeaponCost}";
            _speedCost.text = $"Cost: {_saveSystem.GameData.UpgradeSpeedCost}";
        }

        private void CheckedButton()
        {
            _upgradeWeaponButton.interactable = _saveSystem.GameData.CountCoins >= _saveSystem.GameData.UpgradeWeaponCost;
            _upgradeArmorButton.interactable = _saveSystem.GameData.CountCoins >= _saveSystem.GameData.UpgradeArmorCost;
            _upgradeSpeedButton.interactable = _saveSystem.GameData.CountCoins >= _saveSystem.GameData.UpgradeSpeedCost;
        }
    }
}