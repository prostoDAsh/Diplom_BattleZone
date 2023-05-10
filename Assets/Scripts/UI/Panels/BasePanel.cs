using UnityEngine;
using Zenject;

namespace UI
{
    public class BasePanel : MonoBehaviour
    {
        protected SignalBus signal;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            signal = signalBus;
        }
    }
}