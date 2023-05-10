using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class CurrentLevelPanel : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            ShowPanel().Forget();
        }

        private async UniTaskVoid ShowPanel()
        {
            await _canvasGroup.DOFade(1, 1f);
            await _canvasGroup.DOFade(0, 1f);
        }


    }
}