using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] buttons; // Botões das fases
    public Image[] cadeados; // Imagens dos cadeados
    public Sprite cadeadoAberto; // Sprite do cadeado aberto
    public Sprite cadeadoFechado; // Sprite do cadeado fechado

    void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Debug.Log("CompletedLevel: ");
            Debug.Log(PlayerPrefs.GetInt("CompletedLevel"));
            // Verifica se a fase está bloqueada
            if (i > PlayerPrefs.GetInt("CompletedLevel"))
            {
                buttons[i].interactable = false; // Desativa o botão
                cadeados[i].sprite = cadeadoFechado; // Mostra cadeado fechado
            }
            else
            {
                buttons[i].interactable = true; // Ativa o botão
                cadeados[i].sprite = cadeadoAberto; // Mostra cadeado aberto
            }
        }
    }
}