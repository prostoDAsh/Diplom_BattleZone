using Interfaces;
using UnityEngine;

namespace GameData
{
    public class SaveSystemJson : ISaveSystem
    {
        private const string SAVE_KEY = "GameData";
        
        private GameData _gameData;
        public GameData GameData => _gameData;

        public SaveSystemJson(GameData gameData)
        {
            _gameData = gameData;
        }

        public void Initialize()
        {
            if(PlayerPrefs.HasKey(SAVE_KEY))
                LoadData();
        }
        
        public void SaveData()
        {
            var jsonData = JsonUtility.ToJson(_gameData);
            PlayerPrefs.SetString(SAVE_KEY, jsonData);
        }
        
        public void LoadData()
        {
            var jsonData = PlayerPrefs.GetString(SAVE_KEY);
            _gameData = JsonUtility.FromJson<GameData>(jsonData); // распарсить jsonData(string) в GameData
        }
        
    }
}