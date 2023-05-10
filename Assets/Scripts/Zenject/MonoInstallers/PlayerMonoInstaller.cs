using Player;
using UI;
using UnityEngine;
using Weapon;
using Zenject;

public class PlayerMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<WeaponBase>().FromInstance(GetComponentInChildren<WeaponBase>()).AsSingle();
        Container.Bind<PlayerAnimator>().AsSingle().WithArguments(GetComponent<Animator>());
        Container.Bind<Joystick>().FromComponentInHierarchy().AsSingle();
        Container.Bind<HealthBar>().FromComponentInChildren().AsSingle();

        Container.Bind<Player.Player>().FromInstance(GetComponent<Player.Player>()).AsSingle();
        
        PlayerInstaller.Install(Container);
    }
}