using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Arkanoid
{
    public class ECSStratup : MonoBehaviour
    {
        private DiContainer _container;

        private SystemsGroup _systemGroup;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        private void Start()
        {
            _systemGroup = World.Default.CreateSystemsGroup();

            GameStartSystem gameStartSystem = new GameStartSystem();
            PlayerInputSystem playerInputSystem = new PlayerInputSystem();
            MovingSystem movingSyastem = new MovingSystem();
            BallCollisionSystem ballCollisionSystem = new BallCollisionSystem();
            BlockCollisionSystemSystem blockCollisionSystemSystem = new BlockCollisionSystemSystem();
            LoseConditionSystem loseConditionSystem = new LoseConditionSystem();
            WinConditionSystem winConditionSystem = new WinConditionSystem();


            ScoreSystem scoreSystem = new ScoreSystem();

            DestroyObjectsSystem destroyObjectsSystem = new DestroyObjectsSystem();

            _container.Inject(gameStartSystem);
            _container.Inject(loseConditionSystem);
            _container.Inject(winConditionSystem);
            _container.Inject(scoreSystem);

            
            _systemGroup.AddSystem(gameStartSystem);
            _systemGroup.AddSystem(playerInputSystem);
            _systemGroup.AddSystem(movingSyastem);
            _systemGroup.AddSystem(ballCollisionSystem);
            _systemGroup.AddSystem(blockCollisionSystemSystem);

            _systemGroup.AddSystem(loseConditionSystem);
            _systemGroup.AddSystem(winConditionSystem);

            _systemGroup.AddSystem(scoreSystem);
            _systemGroup.AddSystem(destroyObjectsSystem);


            World.Default.AddSystemsGroup(0, _systemGroup);
        }

        private void OnDestroy()
        {
            World.Default.RemoveSystemsGroup(_systemGroup);
        }
    }
}
