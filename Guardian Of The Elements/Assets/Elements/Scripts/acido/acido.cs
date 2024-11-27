using System.Collections;
using System.Collections.Generic;
using Elements.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class acido : MonoBehaviour
{
    public int damage = 2; // Quantidade de dano causada pelo ácido

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verifica se colidiu com o jogador
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>(); // Obtém o componente Player do jogador
          
            if (player != null)
            {
                player.RestartLevel(SceneManager.GetActiveScene().name); // Aplica o dano ao jogador
            }
        }
    }
}
