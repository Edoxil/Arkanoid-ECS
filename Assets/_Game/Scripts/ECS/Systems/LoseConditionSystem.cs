using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Zenject;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class LoseConditionSystem : ISystem
    {
        private UIController _uIController;
        private bool _loseConditionFound;
        private Filter _loseFilter;
        private Filter _playerFilter;
        private Filter _ballFilter;

        public World World { get; set; }

        [Inject]
        public void Construct(UIController uIController)
        {
            _uIController = uIController;
        }

        public void OnAwake()
        {
            _loseFilter = World.Filter
                 .With<LoseConditionComponent>()
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
            if (_loseConditionFound)
                return;

            if (_loseFilter.IsNotEmpty())
            {
                _loseConditionFound = true;
                _playerFilter.First().AddComponent<UnmovableComponent>();
                _ballFilter.First().AddComponent<UnmovableComponent>();

                World.RemoveEntity(_loseFilter.First());
                _uIController.SwitchScreen<LoseScreen>();
            }

        }

        public void Dispose()
        {

        }

    }

}