using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace BoAdventures
{
    public class InputToActionsSystem : IEcsRunSystem
    {
        [Inject] private EcsPool<MoveDirectionComponent> _moveDirectionsPool;
        [Inject] private EcsPool<KeyPressedEvent> _keyPressedEventsPool;

        private EcsWorld _world;
        private EcsFilter _inputListenersFilter;

        public void Run(EcsSystems systems)
        {
            _world = systems.GetWorld();
            
            EcsFilter keyPressedEventsFilter = _world.Filter<KeyPressedEvent>().End();
            _inputListenersFilter = _world.Filter<MoveDirectionComponent>().Inc<MoveInputListener>().End();

            Vector3 moveDirection = new Vector3(0, 0, 0);

            foreach (int keyPressedEventEntity in keyPressedEventsFilter)
            {
                KeyPressedEvent keyPressedEvent = _keyPressedEventsPool.Get(keyPressedEventEntity);
                                
                if (keyPressedEvent.KeyCode == KeyCode.A)
                {
                    moveDirection += new Vector3(-1, 0, 0);
                }
                if (keyPressedEvent.KeyCode == KeyCode.D)
                {
                    moveDirection += new Vector3(1, 0, 0);
                }
                if (keyPressedEvent.KeyCode == KeyCode.W)
                {
                    moveDirection += new Vector3(0, 0, 1);
                }
                if (keyPressedEvent.KeyCode == KeyCode.S)
                {
                    moveDirection += new Vector3(0, 0, -1);
                }
            }
            
            ChangeMovementDirection(moveDirection);
        }

        private void ChangeMovementDirection(Vector3 moveDirection)
        {
            foreach (int inputListenerEntity in _inputListenersFilter)
            {
                ref MoveDirectionComponent moveDirectionComponent = ref _moveDirectionsPool.Get(inputListenerEntity);
                moveDirectionComponent.MoveDirection = moveDirection;
            }
        }
    }
}