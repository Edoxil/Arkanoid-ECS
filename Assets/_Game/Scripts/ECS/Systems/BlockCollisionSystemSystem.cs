using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class BlockCollisionSystemSystem : ISystem
    {
        private Filter _filter;

        public World World { get; set; }

        public void OnAwake() 
        {
            _filter = World.Filter
                .With<BlockTag>()
                .With<ScoreComponent>()
                .With<CollisionComponent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref CollisionComponent collision = ref entity.GetComponent<CollisionComponent>();

                if (collision.collision == null)
                    continue;

                if (collision.collision.transform.TryGetComponent(out BallTagProvider _))
                {
                    ref ScoreComponent scoreComponent = ref entity.GetComponent<ScoreComponent>();
                    entity.AddComponent<AdditionalScoreComponent>().additionalScore = scoreComponent.score;

                    entity.AddComponent<DestroyedComponent>();
                }
            }

        }

        public void Dispose()
        {
        }
    }
}