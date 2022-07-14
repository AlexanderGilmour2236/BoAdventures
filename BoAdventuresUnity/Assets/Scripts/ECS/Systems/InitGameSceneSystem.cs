using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using Zenject;

namespace BoAdventures
{
    public class InitGameSceneSystem : IEcsInitSystem
    {
        [Inject] private EcsWorld _world;
        [Inject] private EcsPool<PhysicalMoveComponent> _fixedMovablesPool;
        [Inject] private EcsPool<MoveDirectionComponent> _moveDirectionsPool;
        [Inject] private EcsPool<MoveInputListener> _moveInputListenersPool;
        [Inject] private EcsPool<MoveAnimationComponent> _moveAnimationsPool;

        public void Init(EcsSystems systems)
        {                        
            CharacterGO characterGo = GameObject.FindObjectOfType<CharacterGO>();
            int playerCharacterEntity = _world.NewEntity();
            ref PhysicalMoveComponent moveComponent = ref _fixedMovablesPool.Add(playerCharacterEntity);

            moveComponent.Rigidbody = characterGo.RigidBody;
            moveComponent.MoveSpeed = characterGo.MoveSpeed;
            moveComponent.MaxSpeed = characterGo.MaxSpeed;
            moveComponent.SmoothStopMovementSpeed = characterGo.SmoothStopMovementSpeed;
            
            ref MoveAnimationComponent moveAnimationComponent = ref _moveAnimationsPool.Add(playerCharacterEntity);
            moveAnimationComponent.Animator = characterGo.Animator;
            moveAnimationComponent.MoveAnimationBoolName = characterGo.MoveAnimationBoolName;
            
            _moveDirectionsPool.Add(playerCharacterEntity);
            _moveInputListenersPool.Add(playerCharacterEntity);
        }
    }
}