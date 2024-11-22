using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameObject pauseObj;
    public GameObject gameOverObj;
    
    private string nomeFase;

    private bool isPaused;
    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        PauseGame();
    }

    public void CarregarDepoisDe(string nome, float tempo)
    {
        nomeFase = nome;
        Invoke(nameof(CarregarCena),tempo);
    }

    public void CarregarCena()
    {
        SceneManager.LoadScene(nomeFase);
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);
        }

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        gameOverObj.SetActive(true);
        //Time.timeScale = 0f;
        Debug.Log("Game Over ativado!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
}
