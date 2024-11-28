using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void IniciarJogo()
    {
        // Carrega a primeira fase (por exemplo, "Fase1")
        SceneManager.LoadScene("Map");
    }

    public void SairJogo()
    {
        // Sai do jogo
        Application.Quit();
    }
}
