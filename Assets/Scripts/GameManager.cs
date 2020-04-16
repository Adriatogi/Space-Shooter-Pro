using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private bool _isMainMenu = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1); //Current game scene
        }

        if (Input.GetKeyUp(KeyCode.Escape) && (_isMainMenu == false))
        {
            _gameOverPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if(Input.GetKeyUp(KeyCode.Escape) && (_isMainMenu == true))
        {
            Application.Quit();
        }


    }
    public void GameOver()
    {
        _isGameOver = true;
    }

    public void resumeGame()
    {
        _gameOverPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
