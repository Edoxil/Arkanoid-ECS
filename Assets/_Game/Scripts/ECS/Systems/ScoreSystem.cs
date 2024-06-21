using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Zenject;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ScoreSystem : ISystem
    {
        private int _currentScore;
        private UIController _uiController;
        private Filter _filter;

        public World World { get ; set ; }

        [Inject]
        public void Construct(UIController uIController)
        {
            _uiController = uIController;
        }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<AdditionalScoreComponent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref AdditionalScoreComponent additionScore = ref entity.GetComponent<AdditionalScoreComponent>();
                _currentScore += additionScore.additionalScore;
                _uiController.SetScore(_currentScore);

                entity.RemoveComponent<AdditionalScoreComponent>();
            }
        }

        public void Dispose()
        {
        }
    }
}