using UnityEngine;

namespace BoAdventures
{
    public class CharacterGO : MonoBehaviour
    {
        public float MoveSpeed;
        public Rigidbody RigidBody;
        public float MaxSpeed;
        public float SmoothStopMovementSpeed;
        public Animator Animator;
        public string MoveAnimationBoolName;
    }
}