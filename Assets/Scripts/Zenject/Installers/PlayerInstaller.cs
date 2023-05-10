using Player;
using Zenject;

public class PlayerInstaller : Installer<PlayerInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerStateMachine>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<ActivePlayerState>().AsSingle();
        Container.Bind<StartPlayerState>().AsSingle();
        Container.Bind<AttackPlayerState>().AsSingle();
        Container.Bind<DiePlayerState>().AsSingle();
    }
    
}
