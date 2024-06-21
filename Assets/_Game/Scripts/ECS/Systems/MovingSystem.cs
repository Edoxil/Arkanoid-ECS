using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class MovingSystem : IFixedSystem
    {
        public World World { get; set; }

        private Filter _filter;

        public void OnAwake()
        {
            _filter = World.Filter.With<RigidbodyComponent>()
                    .With<DirectionComponent>()
                    .With<SpeedComponent>()
                    .Without<UnmovableComponent>()
                    .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref RigidbodyComponent rigidbody = ref entity.GetComponent<RigidbodyComponent>();
                ref DirectionComponent direction = ref entity.GetComponent<DirectionComponent>();
                ref SpeedComponent speed = ref entity.GetComponent<SpeedComponent>();

                rigidbody.rigidbody.velocity = speed.speed * direction.direction;
            }
        }

        public void Dispose()
        {
        }
    }
}