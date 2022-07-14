using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace BoAdventures
{
    public class KeyBoardPlayerInputSystem : IEcsRunSystem
    {
        [Inject] private EcsWorld _world;
        [Inject] private EcsPool<KeyPressedEvent> _keyPressedEventsPool;
        [Inject] private EcsPool<KeyDownEvent> _keyDownEventsPool;

        public void Run(EcsSystems systems)
        {            
            if (Input.GetKey(KeyCode.A))
            {
                RaiseKeyPressedEvent(KeyCode.A);
            }
            if (Input.GetKey(KeyCode.D))
            {
                RaiseKeyPressedEvent(KeyCode.D);
            }
            if (Input.GetKey(KeyCode.W))
            {
                RaiseKeyPressedEvent(KeyCode.W);
            }
            if (Input.GetKey(KeyCode.S))
            {
                RaiseKeyPressedEvent(KeyCode.S);
            }
            
            if (Input.GetKey(KeyCode.Space))
            {
                RaiseKeyDownEvent(KeyCode.Space);
            }
        }

        private void RaiseKeyPressedEvent(KeyCode keyCode)
        {
            int keyPressedEventEntity = _world.NewEntity();
            ref KeyPressedEvent keyPressedEvent = ref _keyPressedEventsPool.Add(keyPressedEventEntity);
            keyPressedEvent.KeyCode = keyCode;
        }

        private void RaiseKeyDownEvent(KeyCode keyCode)
        {
            int keyDownEventEntity = _world.NewEntity();
            ref KeyDownEvent keyDownEvent = ref _keyDownEventsPool.Add(keyDownEventEntity);
            keyDownEvent.KeyCode = keyCode;
        }
    }
}