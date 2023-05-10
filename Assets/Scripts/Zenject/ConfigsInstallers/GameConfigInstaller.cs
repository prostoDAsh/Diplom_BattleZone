using System;
using Configs;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
{
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private LevelsPrefab levelsPrefab;
  
    public override void InstallBindings()
    {
        Container.BindInstances(playerConfig, levelsPrefab);
    }
    
    [Serializable]
    public class LevelsPrefab
    {
        public Level.Level[] Prefabs;
    }
}