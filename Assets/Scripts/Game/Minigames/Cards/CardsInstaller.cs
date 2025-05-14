using UnityEngine;
using Zenject;

namespace Game.Minigames.Cards
{
    public sealed class CardsInstaller
        : MonoInstaller
    {
        [SerializeField] private CardsConfiguration configuration;
        [SerializeField] private CardsServiceParameters serviceParameters;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CardsService>()
                .AsSingle()
                .WithArguments(configuration, serviceParameters);
        }
    }
}