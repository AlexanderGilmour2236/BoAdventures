using System;

namespace BoAdventures
{
    [Serializable]
    public class CharacterData
    {
        public CharacterAbilitiesData CharacterAbilitiesData;
        public uint CurrentHP;
        
        private MonoCharacterView _characterView;

        public event Action<CharacterData> onTryFire;

        public CharacterData(CharacterAbilitiesData characterAbilitiesData, MonoCharacterView characterView)
        {
            CharacterAbilitiesData = characterAbilitiesData;
            CurrentHP = characterAbilitiesData.MaxHP;
            _characterView = characterView;
        }

        public void OnTryFire()
        {
            onTryFire?.Invoke(this);
        }
        
        public MonoCharacterView CharacterView
        {
            get { return _characterView; }
        }
    }
}