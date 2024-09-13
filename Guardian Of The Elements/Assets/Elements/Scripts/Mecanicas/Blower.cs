using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
       private Rigidbody2D playerRb;
    // Configurações da força do blower
        public float blowerForce = 50f; // A força da corrente de ar
        public bool isActive = true; // Define se o blower está ativo

    private void OnTriggerStay2D(Collider2D collision)
        {
            // Verifica se o objeto que entrou no blower é o jogador
            if (collision.CompareTag("Player") && isActive)
            {
                // Aplica uma força no jogador, elevando-o
                Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.AddForce(Vector2.up * blowerForce, ForceMode2D.Force);
                }
            }
        }
}