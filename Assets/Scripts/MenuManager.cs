using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public static MenuManager Instance;

   public string PlayerName;
   public string HighScorePlayerName;

   public Text HighScoreText;

   public int HighScore;

   private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    class SaveData 
    {
        public string PlayerName;
        public int Points;
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            HighScorePlayerName = data.PlayerName;
            HighScore = data.Points;
            HighScoreText.text = $"Best Score : {HighScorePlayerName} {HighScore}";
        }
    }
}
