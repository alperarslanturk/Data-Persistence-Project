using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuUIHandler : MonoBehaviour
{
    SceneManager sceneManager;
    public InputField PlayerNameField;

    public Text HighScoreText;
    // Start is called before the first frame update
    void Start()
    {
        if(MenuManager.Instance.HighScorePlayerName != null)
        {
            HighScoreText.text = $"Best Score : {MenuManager.Instance.HighScorePlayerName}";
        }
        else
        {
            HighScoreText.text = "Best Score : ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();

        if(UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void PlayerNameEntered()
    {
        MenuManager.Instance.PlayerName = PlayerNameField.text;
    }
}