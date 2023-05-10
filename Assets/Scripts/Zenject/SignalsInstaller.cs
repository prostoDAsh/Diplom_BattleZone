using Signals;
using UI.Buttons;

namespace Zenject
{
    public class SignalsInstaller : Installer<SignalsInstaller> 
    {
        public override void InstallBindings()
        {
            ButtonsSignals();
            PlayerSignals();
            EnemySignals();
            LevelSignals();
        }
        
        private void ButtonsSignals()
        {
            Container.DeclareSignal<RestartLevelSignal>();
            Container.DeclareSignal<StartingGameSignal>();
            Container.DeclareSignal<ReturnMenuSignal>();
            Container.DeclareSignal<NextLevelSignal>();
            
            Container.DeclareSignal<StopCurrentGameSignal>();
            Container.DeclareSignal<GameShopSignal>();
            Container.DeclareSignal<PlayCurrentGameSignal>();
            Container.DeclareSignal<ReklamaSignal>();
            
            Container.DeclareSignal<UpgradeSpeedSignal>();
            Container.DeclareSignal<UpgradeArmorSignal>();
            Container.DeclareSignal<UpgradeWeaponSignal>();
            
            Container.DeclareSignal<ShopPanelSignal>();
            Container.DeclareSignal<ExitSignal>();

            Container.DeclareSignal<BeginnerSignal>();
            Container.DeclareSignal<SoldierSignal>();
            Container.DeclareSignal<KillerSignal>();
            Container.DeclareSignal<ExitAchievementsSignal>();
            Container.DeclareSignal<AchievementsPanelSignal>();
        }
        
        private void PlayerSignals()
        {
            Container.DeclareSignal<PlayerRipSignal>();
            Container.DeclareSignal<PlayerEnteredNextLevelTriggerSignal>();
            Container.DeclareSignal<AddCoinSignal>();
            Container.DeclareSignal<AddStarSignal>();
        }
        
        private void EnemySignals()
        {
            Container.DeclareSignal<EnemyDieSignal>();
        }
        
        private void LevelSignals()
        {
            Container.DeclareSignal<AllEnemiesDiedSignal>();
            Container.DeclareSignal<CurrentLevelSignal>();
        }
        
    }
}