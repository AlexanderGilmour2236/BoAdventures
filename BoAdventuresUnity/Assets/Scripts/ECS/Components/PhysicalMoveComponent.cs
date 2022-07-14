using UnityEngine;

namespace BoAdventures
{
    public struct PhysicalMoveComponent
    {
        public Rigidbody Rigidbody;
        public float MoveSpeed;
        public float MaxSpeed;
        public float SmoothStopMovementSpeed;
    }
}