using BoAdventures;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using Zenject;

public class GameSceneLoader : MonoBehaviour
{
    private EcsSystems _systems;
    private EcsSystems _fixedSystems;
    
    [Inject] private EcsWorld _gameWorld;

    [Inject] private InitGameSceneSystem _initGameSceneSystem;
    [Inject] private KeyBoardPlayerInputSystem _keyBoardPlayerInputSystem;
    [Inject] private InputToActionsSystem _inputToActionsSystem;
    [Inject] private MoveSystem _moveSystem;
    [Inject] private FixedMoveSystem _fixedMoveSystem;
    [Inject] private CharacterMovementAnimationSystem _characterMovementAnimationSystem;

    private void Start()
    {
        _systems = new EcsSystems(_gameWorld);
        _fixedSystems = new EcsSystems(_gameWorld);
        
        // add input systems
        _systems.Add(_initGameSceneSystem);
        _systems.Add(_keyBoardPlayerInputSystem);
        _systems.Add(_inputToActionsSystem);

        // add move systems
        _systems.Add(_moveSystem);
        _fixedSystems.Add(_fixedMoveSystem);
        
        // add animations systems
        _systems.Add(_characterMovementAnimationSystem);

        _systems.DelHere<KeyPressedEvent>();
        _systems.DelHere<KeyDownEvent>();
        
        _systems.Init();
        _fixedSystems.Init();
    }

    private void Update()
    {
        _systems.Run();
    }

    private void FixedUpdate()
    {
        _fixedSystems.Run();
    }
}
