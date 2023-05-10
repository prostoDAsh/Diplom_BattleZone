using UnityEngine;

namespace Level
{
    public class NextLevelTrigger : MonoBehaviour
    {
        private ParticleSystem _particle;
        private void Awake()
        {
            _particle = GetComponentInChildren<ParticleSystem>();
        }

        public void InitParticle()
        {
            _particle.Play();
        }
    }
}