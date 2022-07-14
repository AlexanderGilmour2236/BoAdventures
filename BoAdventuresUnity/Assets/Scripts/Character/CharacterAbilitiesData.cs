using System;

namespace BoAdventures
{
    [Serializable]
    public class CharacterAbilitiesData
    {
        public float Speed = 5; 
        public float MaxSpeed = 5;
        public float SmoothStopMovementSpeed = 0.3f ;
        public float CharacterRange = 5;
        public float CharacterShotDelay = 0.1f;
        public uint MaxHP = 6;
    }
}