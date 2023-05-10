using UnityEngine;

namespace Level
{
    public class StartPlayerPoint : MonoBehaviour
    {
        private ParticleSystem _particle;

        private void Awake()
        {
            _particle = GetComponentInChildren<ParticleSystem>();
        }

        public void PlayParticle()
        {
            _particle.Play();
        }
    }
}