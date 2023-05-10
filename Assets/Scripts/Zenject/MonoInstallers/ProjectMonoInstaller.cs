using ADS;
using GameData;
using Interfaces;
using Zenject;

public class ProjectMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.BindInterfacesTo<ProjectSetupInstaller>().AsSingle().NonLazy();
        BindingAds();
        BindingSaveSystemAndGameData();
    }

    private void BindingSaveSystemAndGameData()
    {
        Container.Bind<GameData.GameData>().AsSingle().NonLazy();
        Container.Bind<ISaveSystem>().To<SaveSystemJson>().AsSingle().NonLazy();
    }

    private void BindingAds()
    {
       Container.BindInterfacesAndSelfTo<InterstitialAdExample>().AsSingle().NonLazy();
       Container.BindInterfacesAndSelfTo<AdsInitializer>().AsSingle().NonLazy();
    }
}