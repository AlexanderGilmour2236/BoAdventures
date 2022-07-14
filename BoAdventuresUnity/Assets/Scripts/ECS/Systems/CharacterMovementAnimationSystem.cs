using Leopotam.EcsLite;
using Zenject;

namespace BoAdventures
{
    public class CharacterMovementAnimationSystem : IEcsRunSystem
    {
        [Inject] private EcsWorld _world;
        [Inject] private EcsPool<MoveDirectionComponent> _moveDirectionsPool;
        [Inject] private EcsPool<MoveAnimationComponent> _moveAnimatorsPool;
        
        public void Run(EcsSystems systems)
        {
            EcsFilter animatedMovablesFilter = _world.Filter<MoveDirectionComponent>().Inc<MoveAnimationComponent>().End();

            foreach (int animatedMovableEntity in animatedMovablesFilter)
            {
                MoveAnimationComponent moveAnimationComponent = _moveAnimatorsPool.Get(animatedMovableEntity);
                MoveDirectionComponent moveDirectionComponent = _moveDirectionsPool.Get(animatedMovableEntity);
                
                moveAnimationComponent.Animator.SetBool(moveAnimationComponent.MoveAnimationBoolName, moveDirectionComponent.MoveDirection.sqrMagnitude > 0);
            }
        }
    }
}