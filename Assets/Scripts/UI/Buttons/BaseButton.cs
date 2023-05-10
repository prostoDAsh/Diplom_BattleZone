using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons
{
    public abstract class BaseButton<T> : MonoBehaviour where T : struct
    {
        private Button _button;
        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _signalBus.Fire<T>();
        }
    }
}