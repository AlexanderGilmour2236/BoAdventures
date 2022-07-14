using System.Collections;
using System.Collections.Generic;
using BoAdventures;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
   private EcsWorld _world;

   public override void InstallBindings()
   {
      _world = new EcsWorld();
      Container.Bind<EcsWorld>().FromInstance(_world);
      
      BindPool<MoveComponent>();
      BindPool<PhysicalMoveComponent>();
      BindPool<MoveDirectionComponent>();
      BindPool<KeyDownEvent>();
      BindPool<KeyPressedEvent>();
      BindPool<MoveInputListener>();
      BindPool<MoveAnimationComponent>();

      Container.Bind<InitGameSceneSystem>().AsSingle().NonLazy();
      Container.Bind<InputToActionsSystem>().AsSingle().NonLazy();
      Container.Bind<KeyBoardPlayerInputSystem>().AsSingle().NonLazy();
      Container.Bind<MoveSystem>().AsSingle().NonLazy();
      Container.Bind<FixedMoveSystem>().AsSingle().NonLazy();
      Container.Bind<CharacterMovementAnimationSystem>().AsSingle().NonLazy();
   }

   private void BindPool<T>() where T : struct
   {
      Container.Bind<EcsPool<T>>().FromMethod(() => _world.GetPool<T>());
   }
}
