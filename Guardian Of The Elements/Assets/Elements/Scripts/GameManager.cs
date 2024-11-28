using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public int totalScore = 0;
    public Text scoreText;
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
    public void UpdateScore(int value)
    {
        Debug.Log("Chegou aqui");
        score += value;
        scoreText.text = score.ToString();

        PlayerPrefs.SetInt("score", score + totalScore);
    }
    public void CarregarDepoisDe(string nome, float tempo)
    {
        nomeFase = nome;
        Invoke(nameof(CarregarCena), tempo);
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
        //Time.timeScale = 1f;
        Debug.Log("Game Over ativado!");
    }

    public void RestartGame()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Reiniciarcena()
    {
        SceneManager.LoadScene(0);
    }
}