using Scellecs.Morpeh;
using UnityEngine;

namespace Arkanoid
{
    public sealed class DestroyObjectsSystem : ICleanupSystem
    {
        private Filter _filter;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<DestroyedComponent>()
                .With<TransformComponent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
                Object.Destroy(transform.transform.gameObject);

                World.RemoveEntity(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}