using UnityEngine;

namespace BoAdventures
{
    public interface ICharacterView
    {
        void TakeDamage();
        CharacterData CharacterData { get; }
    }
}