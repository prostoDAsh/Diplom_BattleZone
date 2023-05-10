using Costs;
using Interfaces;
using Signals;
using TMPro;
using UI.Buttons;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AchievementsPanel : MonoBehaviour
    {
        private ISaveSystem _saveSystem;
        private const int BeginnerCost = 5;
        private const int SoldierCost = 15;
        private const int KillerCost = 30;
        
        private SignalBus _signalBus;
        private TextMeshProUGUI _star;

        private Button _beginnerButton;
        private Button _soldierButton;
        private Button _killerButton;

        private cost1 _cost1;
        private cost2 _cost2;
        private cost3 _cost3;
        

        [Inject]

        public void Construct(ISaveSystem saveSystem, SignalBus signalBus)
        {
            _saveSystem = saveSystem;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _star = GetComponentInChildren<StarText>().GetComponent<TextMeshProUGUI>();
            _beginnerButton = GetComponentInChildren<BeginnerButton>().GetComponent<Button>();
            _soldierButton = GetComponentInChildren<SoldierButton>().GetComponent<Button>();
            _killerButton = GetComponentInChildren<KillerButton>().GetComponent<Button>();
            _cost1 = GetComponentInChildren<cost1>();
            _cost2 = GetComponentInChildren<cost2>();
            _cost3 = GetComponentInChildren<cost3>();
        }

        private void OnEnable()
        {
            StarShow();
        }

        private void Start()
        {
            _signalBus.Subscribe<BeginnerSignal>(BuyBeginner);
            _signalBus.Subscribe<SoldierSignal>(BuySoldier);
            _signalBus.Subscribe<KillerSignal>(BuyKiller);

            BuyBeginner();
            BuySoldier();
            BuyKiller();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<BeginnerSignal>(BuyBeginner);
            _signalBus.Unsubscribe<SoldierSignal>(BuySoldier);
            _signalBus.Unsubscribe<KillerSignal>(BuyKiller);
        }

        private void BuyBeginner()
        {
            if (_saveSystem.GameData.CountStars >= BeginnerCost|| _saveSystem.GameData.IsBeginner)
            {
                _beginnerButton.image.color = Color.white;
                _beginnerButton.interactable = false;
                
                _cost1.gameObject.SetActive(false);

                if (!_saveSystem.GameData.IsBeginner)
                {
                    _saveSystem.GameData.CountStars -= BeginnerCost;
                    _saveSystem.GameData.IsBeginner = true;
                    StarShow();
                }
            }
        }

        private void BuySoldier()
        {
            
            if (_saveSystem.GameData.CountStars >= SoldierCost || _saveSystem.GameData.IsSoldier)
            {
                _soldierButton.image.color = Color.white;
                _soldierButton.interactable = false;
                
                _cost2.gameObject.SetActive(false);
            
                if (!_saveSystem.GameData.IsSoldier)
                {
                    _saveSystem.GameData.CountStars -= SoldierCost;
                    _saveSystem.GameData.IsSoldier = true;
                    StarShow();
                }
            }
        }
        
        private void BuyKiller()
        {
            if (_saveSystem.GameData.CountStars >= KillerCost|| _saveSystem.GameData.IsKiller)
            {
                _killerButton.image.color = Color.white;
                _killerButton.interactable = false;
                
                _cost3.gameObject.SetActive(false);

                if (!_saveSystem.GameData.IsKiller)
                {
                    _saveSystem.GameData.CountStars -= KillerCost;
                    _saveSystem.GameData.IsKiller = true;
                    StarShow();
                }
            }
        }

        private void StarShow()
        {
            _star.text = _saveSystem.GameData.CountStars.ToString();
        }
    }
}