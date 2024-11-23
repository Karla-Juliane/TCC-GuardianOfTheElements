using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    public float fallDelay = 0.5f; // Tempo até a plataforma começar a cair
    public float respawnDelay = 1f; // Tempo para a plataforma reaparecer
    private Vector2 initialPosition; // Posição inicial da plataforma

    private bool isPlayerOnPlatform = false; // Verifica se o jogador está sobre a plataforma

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        initialPosition = transform.position; // Salva a posição inicial
    }

    private void Update()
    {
        // Evita que o jogador "grude" na plataforma ao cair
        if (rb.bodyType == RigidbodyType2D.Dynamic && isPlayerOnPlatform)
        {
            // Libera o jogador caso a plataforma esteja caindo
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            player.parent = null;
            isPlayerOnPlatform = false;
        }
    }

    private IEnumerator FallAndRespawn()
    {
        // Aguarda antes de começar a cair
        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic; // Torna a plataforma dinâmica para cair

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
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            collision.transform.parent = transform; // Define o jogador como filho da plataforma
            StartCoroutine(FallAndRespawn());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            collision.transform.parent = null; // Remove o jogador como filho da plataforma
        }
    }
}
