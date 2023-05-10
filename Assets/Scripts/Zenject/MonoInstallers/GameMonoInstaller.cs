using Enemy;
using Interfaces;
using UnityEngine;
using Zenject;


public class GameMonoInstaller : MonoInstaller
{
    [Header("Enemy")]
    [SerializeField] private EnemyDemon enemyDemon;
    [SerializeField] private EnemyHard enemyHard;
    [SerializeField] private EnemyEasy enemyEasy;
    
    public override void InstallBindings()
    {
        SignalsInstaller.Install(Container);
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
        
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle()
            .WithArguments(enemyEasy, enemyHard, enemyDemon);
    }
}

