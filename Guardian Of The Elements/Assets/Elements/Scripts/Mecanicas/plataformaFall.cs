using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    public float fallDelay = 2f; // Tempo até a plataforma começar a cair
    public float respawnDelay = 3f; // Tempo para a plataforma reaparecer
    private Vector2 initialPosition; // Posição inicial da plataforma

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        initialPosition = transform.position; // Salva a posição inicial
    }

    private IEnumerator FallAndRespawn()
    {
        // Aguarda antes de começar a cair
        yield return new WaitForSeconds(fallDelay);

        // Torna o Rigidbody dinâmico para começar a cair
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Aguarda para a queda ocorrer
        yield return new WaitForSeconds(1f);

        // Desativa a plataforma após a queda
        spriteRenderer.enabled = false; // Oculta o sprite
        boxCollider.enabled = false; // Desativa o colisor
        rb.bodyType = RigidbodyType2D.Static; // Reseta o Rigidbody para estático

        // Aguarda antes de reaparecer
        yield return new WaitForSeconds(respawnDelay);

        // Restaura a plataforma
        transform.position = initialPosition; // Volta para a posição inicial
        spriteRenderer.enabled = true; // Reativa o sprite
        boxCollider.enabled = true; // Reativa o colisor
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o jogador está sobre a plataforma
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAndRespawn());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Desvincula o jogador da plataforma
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
