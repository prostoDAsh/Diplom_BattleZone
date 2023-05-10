using UI;
using Zenject;

public class UIMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MenuPanel>().FromComponentInChildren().AsSingle();
        Container.Bind<GamePanel>().FromComponentInChildren().AsSingle();
        Container.Bind<WinPanel>().FromComponentInChildren().AsSingle();
        Container.Bind<FailPanel>().FromComponentInChildren().AsSingle();
        Container.Bind<Shop>().FromComponentInChildren().AsSingle();
        Container.Bind<PausePanel>().FromComponentInChildren().AsSingle();
        Container.Bind<AchievementsPanel>().FromComponentInChildren().AsSingle();
        
        Container.Bind<UIManager>().AsSingle();
    }
}