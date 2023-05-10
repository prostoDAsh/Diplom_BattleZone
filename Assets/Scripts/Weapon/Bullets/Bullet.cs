using Enums;
using Level;
using UnityEngine;

namespace Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int damage;

        private BulletType _type;
        private bool _isActive;
        private Vector3 _directionShoot = Vector3.zero;
        public BulletType Type => _type;

        public int Damage => damage;
        
        public void Init(Vector3 target, BulletType type)
        {
            _type = type;
            _directionShoot = (new Vector3(target.x, transform.position.y, target.z) - transform.position).normalized;
            _isActive = true;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if(!_isActive) 
                return;
            
            transform.Translate(_directionShoot * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Player.Player>() 
                && !other.gameObject.GetComponent<Wall>()
                && !other.gameObject.GetComponent<Enemy.EnemyBase>()) return;
            
            gameObject.SetActive(false);
            _isActive = false;
        }
    }
}