using UnityEngine;

namespace BoAdventures
{
    public class MonoCharacterView : MonoBehaviour, ICharacterView
    {
        public virtual void TakeDamage() { }
        public virtual CharacterData CharacterData { get; }
    }
}