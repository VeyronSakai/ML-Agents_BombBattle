using UnityEngine;
using Zenject;

namespace Manager
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GameStateManager>()
                .AsSingle();
        }
    }
}