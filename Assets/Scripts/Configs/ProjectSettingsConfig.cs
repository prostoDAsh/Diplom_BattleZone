using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ProjectSettingsConfig", menuName = "Configs/Zenject/ProjectSettingsConfig", order = 0)]
    public class ProjectSettingsConfig : ScriptableObject
    {
        [SerializeField] private int targetFps;

        public int TargetFps => targetFps;
    }
}