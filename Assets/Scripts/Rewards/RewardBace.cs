using DG.Tweening;
using UnityEngine;

namespace Rewards
{
    public class RewardBace : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] protected float jumpPower;
        [SerializeField] protected float durationRotate = 4f;
        [SerializeField] protected float durationJump = 1.5f;

        private Sequence _sequence;
        protected virtual void Start()
        {
            _sequence = DOTween.Sequence();
            ActiveReward();
        }

        private void ActiveReward()
        {
            var directionX = Random.Range(0.5f, 1f);
            var directionZ = Random.Range(0.5f, 1f);
            
            transform.DOJump(transform.position + new Vector3(directionX, 0, directionZ), jumpPower, 1, durationJump);
            _sequence.Append(transform.DORotate(new Vector3(0, 360, 0), durationRotate, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear));
            _sequence.SetLoops(-1);
        }

        private void OnDisable()
        {
            _sequence?.Kill();
        }
    }
}