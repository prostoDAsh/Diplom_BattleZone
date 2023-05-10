using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float delayBetweenAttacks = 2f;
        [SerializeField] private float armor = 20f;
        [SerializeField] private float speed = 1.5f;
        [SerializeField] private float rotateSpeed = 10f;
        
        private float _delayBetweenAttacks;
        private float _armor;
        private float _speed;

        public float DelayBetweenAttacks => _delayBetweenAttacks;
        public float Armor => _armor;
        public float Speed => _speed;
        public float RotateSpeed => rotateSpeed;

        public void Init()
        {
            _delayBetweenAttacks = delayBetweenAttacks;
            _armor = armor;
            _speed = speed;
        }


        public void AddArmor(float value)
        {
            _armor += value;
        }

        public void AddSpeed(float value)
        {
            _speed += value;
        }

        public void ReductionDelayBetweenAttacks(float value)
        {
            _delayBetweenAttacks -= value;
            Debug.Log(_delayBetweenAttacks);
        }
    }
}