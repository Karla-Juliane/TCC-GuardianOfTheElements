using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage = 2; // Quantidade de dano causada pelo espinho

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verifica se colidiu com o jogador
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>(); // Obtém o componente Player do jogador

            if (player != null)
            {
                player.Demage(damage); // Chama a função TakeDamage do jogador passando a quantidade de dano
            }
        }
    }
}