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
    
        
        public CharacterAbilitiesData CharacterAbilitiesData
        {
            get { return _characterAbilitiesData; }
        }
    }
}
