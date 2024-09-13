using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyIce : MonoBehaviour
{
    public float slidingFriction = 0.1f;  // Atrito quando o jogador está no gelo
    private float originalFriction;        // Atrito original do jogador
    private Rigidbody2D playerRb;

    // Detecta quando o jogador entra na superfície escorregadia
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // Guarda o atrito original e ajusta para um valor baixo
                originalFriction = playerRb.sharedMaterial.friction;
                playerRb.sharedMaterial.friction = slidingFriction;
                Debug.Log("Jogador entrou na superfície escorregadia.");
            }
        }
    }

    // Detecta quando o jogador sai da superfície escorregadia
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerRb != null)
            {
                // Restaura o atrito original
                playerRb.sharedMaterial.friction = originalFriction;
                Debug.Log("Jogador saiu da superfície escorregadia.");
            }
        }
    }
}