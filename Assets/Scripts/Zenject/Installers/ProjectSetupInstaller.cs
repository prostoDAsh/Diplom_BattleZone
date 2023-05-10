using Configs;
using UnityEngine;
using Zenject;

    public class ProjectSetupInstaller : IInitializable
    {
        private readonly ProjectSettingsConfig _projectSettingsConfig;

        public ProjectSetupInstaller(ProjectSettingsConfig projectSettingsConfig)
        {
            _projectSettingsConfig = projectSettingsConfig;
        }
        
        public void Initialize()
        {
            Application.targetFrameRate = _projectSettingsConfig.TargetFps;
        }
    }