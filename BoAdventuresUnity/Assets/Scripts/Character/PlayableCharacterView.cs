using UnityEngine;

namespace BoAdventures
{
    public class PlayableCharacterView : MonoCharacterView
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
    
        private CharacterData _characterData;
    
        private CharacterAbilitiesData _characterAbilitiesData;
        private Vector3 _moveDirection;
        
        public void SetData(CharacterData characterData)
        {
            _characterData = characterData;
            _characterAbilitiesData = characterData.CharacterAbilitiesData;
        }
        
        void Update()
        {
            _moveDirection = Vector3.zero;
            
            if (Input.GetKey(KeyCode.A))
            {
                _moveDirection.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _moveDirection.x += 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                _moveDirection.z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _moveDirection.z -= 1;
            }
    
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TryFire();
            }
            
            _animator.SetBool("CharacterMove", _moveDirection.sqrMagnitude > 0);
        }
    
        private void TryFire()
        {
            _characterData.OnTryFire();
        }
    
        private void FixedUpdate()
        {
            if (_moveDirection.sqrMagnitude > 0)
            {
                MoveCharacter(_moveDirection);
            }
            else
            {
                SmoothStopMovement();
            }
        }
    
        private void SmoothStopMovement()
        {
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, _characterAbilitiesData.SmoothStopMovementSpeed);
        }
    
        private void MoveCharacter(Vector3 direction)
        {
            _rigidbody.AddForce(direction * _characterAbilitiesData.Speed, ForceMode.VelocityChange);
            ClampSpeed();
        }
    
        private void ClampSpeed()
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _characterAbilitiesData.MaxSpeed);
        }
        
        public CharacterAbilitiesData CharacterAbilitiesData
        {
            get { return _characterAbilitiesData; }
        }
    
        public void TakeDamage()
        {
            
        }
        
        public CharacterData CharacterData
        {
            get { return _characterData; }
        }
    }
}
