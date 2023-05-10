namespace Interfaces
{
    public interface ISaveSystem
    {
        public GameData.GameData GameData { get; }  
        public void Initialize();
        public void SaveData();
        public void LoadData();
    }
}