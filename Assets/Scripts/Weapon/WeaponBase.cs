using Enums;
using Pool;
using UnityEngine;

namespace Weapon

{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected Bullet bulletPrefab;
        [SerializeField] protected int countBullets = 10;
        
        private ShootPoint _shootPoint;
        private ParticleSystem _particle;
        private Pool<Bullet> _poolBullets;
        
        private void Awake()
        {
            _particle = GetComponentInChildren<ParticleSystem>();
            _shootPoint = GetComponentInChildren<ShootPoint>();
        }

        private void Start()
        {
            _poolBullets = new Pool<Bullet>(bulletPrefab, countBullets);
        }

        public virtual void Fire(Vector3 direction, BulletType type)
        {
            if (_poolBullets.TryGetObject(out GameObject bullet))
            {
                bullet.transform.position = _shootPoint.transform.position;
                
                bullet.GetComponent<Bullet>().Init(direction, type);
                _particle.Play();
            }
        }

        private void OnDestroy()
        {
            Destroy(_poolBullets.Parent);
        }
    }
}