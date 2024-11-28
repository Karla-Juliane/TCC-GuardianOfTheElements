using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FimDeJogo : MonoBehaviour
{
    public void IniciarJogo()
    {
        // Carrega a primeira fase (por exemplo, "Fase1")
        SceneManager.LoadScene("MenuInicial");
    }

    public void SairJogo()
    {
        // Sai do jogo
        Application.Quit();
    }
}
