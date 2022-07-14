using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace BoAdventures
{
    public class MoveSystem : IEcsRunSystem
    {
        [Inject] private EcsPool<MoveDirectionComponent> _moveDirectionsPool;
        [Inject] private EcsPool<MoveComponent> _movablesPool;

        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter movablesWithInputListenersFilter =
                world.Filter<MoveComponent>().Inc<MoveDirectionComponent>().End();
           
            foreach (int movableEntity in movablesWithInputListenersFilter)
            {
                MoveDirectionComponent moveDirectionComponent = _moveDirectionsPool.Get(movableEntity);
                ref MoveComponent moveComponent = ref _movablesPool.Get(movableEntity);
                
                moveComponent.Transform.Translate(moveDirectionComponent.MoveDirection * moveComponent.MoveSpeed * Time.deltaTime);
            }
        }
    }
}