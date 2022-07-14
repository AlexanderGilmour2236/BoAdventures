using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BoAdventures
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] BulletView bulletPrefab;
        [SerializeField] Camera _camera;
        
        private List<PropFireTarget> _enemies;
        private PlayableCharacterView _playableCharacterView;
        private DateTime _lastShotTime;

        private CharacterData _playerCharacterData;
        private CharacterAbilitiesData _playerCharacterAbilitiesData = new CharacterAbilitiesData();

        private Tween _cameraShakeTween;
        
        private void Start()
        {
            LoadLevel();
        }

        private void LoadLevel()
        {
            _playableCharacterView = FindObjectOfType<PlayableCharacterView>();
            _playerCharacterData = new CharacterData(_playerCharacterAbilitiesData, _playableCharacterView);
            _playableCharacterView.SetData(_playerCharacterData);
            
            _playerCharacterData.onTryFire += OnPlayableCharacterTryFire;

            _enemies = new List<PropFireTarget>(FindObjectsOfType<PropFireTarget>());
            _lastShotTime = DateTime.Now.AddSeconds(-1 * _playableCharacterView.CharacterAbilitiesData.CharacterShotDelay);
        }

        private void OnPlayableCharacterTryFire(CharacterData characterData)
        {
            PropFireTarget propFireTarget = FindTargetCharacter();
            
            if (propFireTarget != null && (DateTime.Now - _lastShotTime).TotalSeconds >= _playableCharacterView.CharacterAbilitiesData.CharacterShotDelay)
            {
                _lastShotTime = DateTime.Now;
                ShootAtEnemy(characterData.CharacterView.transform.position, propFireTarget.transform.position, characterData.CharacterAbilitiesData.CharacterRange);
            }
        }

        private void ShootAtEnemy(Vector3 playableCharacterPosition, Vector3 enemyCharacterPosition, float characterRange)
        {
            BulletView bulletView = Instantiate(bulletPrefab, playableCharacterPosition, Quaternion.LookRotation(enemyCharacterPosition - playableCharacterPosition));
            bulletView.ShootTo(_playableCharacterView.transform.position, enemyCharacterPosition, characterRange);
            
            SubscribeBullet(bulletView);
            ShakeScreenFromFire();
        }

        private void ShakeScreenFromFire()
        {
            if (_cameraShakeTween != null)
            {
                _cameraShakeTween.Complete(true);
            }

            _cameraShakeTween = _camera.DOShakePosition(0.1f, 0.5f).OnComplete(() => _cameraShakeTween = null);
        }

        private void SubscribeBullet(BulletView bulletView)
        {
            bulletView.onBulletReachEnemy += OnBulletReachEnemy;
        }
        
        private void UnsubscribeBullet(BulletView bulletView)
        {
            bulletView.onBulletReachEnemy -= OnBulletReachEnemy;
        }

        private void OnBulletReachEnemy(BulletView bulletView, PropFireTarget propFireTarget)
        {
            UnsubscribeBullet(bulletView);
            Destroy(bulletView.gameObject);
        }

        private PropFireTarget FindTargetCharacter()
        {
            float minDistance = float.MaxValue;
            PropFireTarget propFireTarget = null;
            
            foreach (PropFireTarget enemyCharacter in _enemies)
            {
                float distanceToEnemy =
                    Vector3.Distance(enemyCharacter.transform.position, _playableCharacterView.transform.position);
                
                if (distanceToEnemy < minDistance)
                {
                    minDistance = distanceToEnemy;
                    propFireTarget = enemyCharacter;
                }            
            }

            return propFireTarget;
        }
    }
}
