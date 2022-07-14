using System;
using UnityEngine;

namespace BoAdventures
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;

        private bool _isShooting;
        private Vector3 _targetPosition;
        private float _characterRange;
        private Vector3 _startPoint;
        
        public event Action<BulletView, PropFireTarget> onBulletReachEnemy;

        public void ShootTo(Vector3 startPosition, Vector3 targetPosition, float characterRange)
        {
            _startPoint = startPosition;
            _targetPosition = targetPosition;
            _isShooting = true;
            _characterRange = characterRange;
        }

        private void Update()
        {
            if (_isShooting)
            {
                transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime, Space.Self);
                if (Vector3.Distance(_startPoint, transform.position) > _characterRange)
                {
                    DestroyBullet();
                }
            }
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                onBulletReachEnemy?.Invoke(this, other.GetComponent<PropFireTarget>());
            }
        }
    }
}