using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Zenject;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class GameStartSystem : ISystem
    {
        private UIController _uIController;
        private BlockSpawner _blockSpawner;
        private bool _gameStarted;

        private Filter _ballFilter;
        private Filter _platformFilter;

        public World World { get; set; }

        [Inject]
        public void Construct(UIController uIController, BlockSpawner blockSpawner)
        {
            _uIController = uIController;
            _blockSpawner = blockSpawner;
        }

        public void OnAwake()
        {
            _ballFilter = World.Filter
                .With<BallTag>()
                .With<RigidbodyComponent>()
                .With<TransformComponent>()
                .With<UnmovableComponent>()
                .Build();

            _platformFilter = World.Filter
                .With<PlayerTag>()
                .With<TransformComponent>()
                .Build();

            _uIController.SwitchScreen<StartScreen>();
            _blockSpawner.SpawnRandomMap();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_gameStarted == false)
            {
                Entity ball = _ballFilter.First();
                ref RigidbodyComponent ballRigidbody = ref ball.GetComponent<RigidbodyComponent>();
                ref TransformComponent ballTransform = ref ball.GetComponent<TransformComponent>();

                Entity platform = _platformFilter.First();
                ref TransformComponent platformTransform = ref platform.GetComponent<TransformComponent>();

                ballTransform.transform.position = platformTransform.transform.position + Vector3.up * 0.3f;


                if (Input.GetKeyDown(KeyCode.Space))
                {

                    ballTransform.transform.parent = null;
                    ballRigidbody.rigidbody.isKinematic = false;

                    ball.RemoveComponent<UnmovableComponent>();

                    _uIController.SwitchScreen<GameplayScreen>();
                    _gameStarted = true;
                }

            }
        }

        public void Dispose()
        {
        }

    }
}