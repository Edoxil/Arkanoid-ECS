using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerInputSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;

        private Vector3 _inputDirection;


        public void OnAwake()
        {
            _filter = World.Filter.With<PlayerTag>()
                .With<DirectionComponent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            _inputDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _inputDirection.x = -1f;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _inputDirection.x = 1f;
            }


            ref DirectionComponent direction = ref _filter.First().GetComponent<DirectionComponent>();
            direction.direction = _inputDirection;
        }

        public void Dispose()
        {

        }
    }
}