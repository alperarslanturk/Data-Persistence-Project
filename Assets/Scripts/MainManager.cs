using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public string highScorePlayerName;
    public int highScore;
    public Text HighScoreText;

    
    // Start is called before the first frame update
    void Start()
    {
        LoadHighScore();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        ScoreText.text = $"{MenuManager.Instance.PlayerName} Score : 0";
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if(m_Points>highScore)
            {
                highScore = m_Points;
                highScorePlayerName = MenuManager.Instance.PlayerName;
                HighScoreText.text = $" Best Score : {highScorePlayerName} {highScore}";
                SaveHighScore();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{MenuManager.Instance.PlayerName} Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    [System.Serializable]
    class SaveData 
    {
        public string PlayerName;
        public int Points;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.PlayerName = MenuManager.Instance.PlayerName;
        data.Points = m_Points;
        MenuManager.Instance.HighScorePlayerName = MenuManager.Instance.PlayerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScorePlayerName = data.PlayerName;
            highScore = data.Points;
            HighScoreText.text = $"Best Score : {highScorePlayerName} {highScore}";
        }
    }
    
}
