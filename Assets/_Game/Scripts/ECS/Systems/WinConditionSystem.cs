using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Zenject;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class WinConditionSystem : ISystem
    {
        private Filter _filter;
        private UIController _uIController;
        private bool _winConditionFound;
        private Filter _playerFilter;
        private Filter _ballFilter;
        public World World { get ; set ; }

        [Inject]
        public void Construct(UIController uIController)
        {
            _uIController = uIController;
        }

        public void OnAwake()
        {
            _filter = World.Filter
                 .With<BlockTag>()
                 .Build();

            _playerFilter = World.Filter
                 .With<PlayerTag>()
                 .Build();

            _ballFilter = World.Filter
                 .With<BallTag>()
                 .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_winConditionFound)
                return;

            if(_filter.IsEmpty())
            {
                _winConditionFound = true;

                _playerFilter.First().AddComponent<UnmovableComponent>();
                _ballFilter.First().AddComponent<UnmovableComponent>();

                _uIController.SwitchScreen<WinScreen>();
            }

        }

        public void Dispose()
        {

        }
    }

}