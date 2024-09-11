using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private string nomeFase;
    private void Awake()
    {
        instance = this;
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
}
