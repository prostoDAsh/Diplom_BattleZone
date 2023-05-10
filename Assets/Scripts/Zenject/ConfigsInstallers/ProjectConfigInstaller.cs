using Configs;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectConfigInstaller", menuName = "Installers/ProjectConfigInstaller")]
public class ProjectConfigInstaller : ScriptableObjectInstaller<ProjectConfigInstaller>
{
    [SerializeField] private ProjectSettingsConfig projectSettingsConfig;
    public override void InstallBindings()
    {
        Container.BindInstance(projectSettingsConfig);
    }
}