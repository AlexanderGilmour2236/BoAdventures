using UnityEngine;

namespace BoAdventures
{
    public interface ITargetObject
    {
        FireTargetType FireTargetType { get; }
        Transform Transform { get; }
        void TakeDamage(int damage);
    }
}