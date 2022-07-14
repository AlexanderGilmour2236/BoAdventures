using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace BoAdventures
{
    public class FixedMoveSystem : IEcsRunSystem
    {
        [Inject] private EcsPool<MoveDirectionComponent> _moveDirectionsPool;
        [Inject] private EcsPool<PhysicalMoveComponent> _fixedMovablesPool;

        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter movablesWithInputListenersFilter =
                world.Filter<PhysicalMoveComponent>().Inc<MoveDirectionComponent>().End();
           
            foreach (int movableEntity in movablesWithInputListenersFilter)
            {
                MoveDirectionComponent moveDirectionComponent = _moveDirectionsPool.Get(movableEntity);
                ref PhysicalMoveComponent moveComponent = ref _fixedMovablesPool.Get(movableEntity);

                Vector3 moveDirection = moveDirectionComponent.MoveDirection;
                
                if (moveDirection.sqrMagnitude > 0)
                {
                    MoveCharacter(moveComponent.Rigidbody, moveDirection, moveComponent.MoveSpeed, moveComponent.MaxSpeed);
                }
                else
                {
                    SmoothStopMovement(moveComponent.Rigidbody, moveComponent.SmoothStopMovementSpeed);
                }
            }
        }
    
        private void SmoothStopMovement(Rigidbody rigidbody, float smoothStopMovementSpeed)
        {
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, smoothStopMovementSpeed);
        }
    
        private void MoveCharacter(Rigidbody rigidbody, Vector3 direction, float moveSpeed, float maxSpeed)
        {
            rigidbody.AddForce(direction * moveSpeed, ForceMode.VelocityChange);
            ClampSpeed(rigidbody, maxSpeed);
        }
    
        private void ClampSpeed(Rigidbody rigidbody, float maxSpeed)
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
    }
}