using TriInspector;
using UnityEngine;
using Zenject;

namespace Arkanoid
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField, Required] private UIController _uIController;
        [SerializeField, Required] private BlockSpawner _blockSpawner;

        public override void InstallBindings()
        {
            BindUiController();
            BindBlockSpawner();
        }

        private void BindBlockSpawner()
        {
            Container.Bind<BlockSpawner>()
                .FromInstance(_blockSpawner)
                .AsSingle()
                .NonLazy();
        }

        private void BindUiController()
        {
            Container.Bind<UIController>()
                .FromInstance(_uIController)
                .AsSingle()
                .NonLazy();
        }
    }
}