using System;
using UnityEngine;

namespace Level
{
    public class PointEnemy : MonoBehaviour
    {
        private Transform _twoPoint;

        public Transform TwoPoint => _twoPoint;

        private void Awake()
        {
            _twoPoint = GetComponentInChildren<MovePoint>().transform;
        }
    }
}