using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float moveSpeed;
    public bool platform1, platform2;
    public bool moveRight = true, moveUp = true;

    private Vector2 movementDirection;

    void Update()
    {
        // Movimento horizontal
        if (platform1)
        {
            if (transform.position.x > 306.55f)
            {
                moveRight = false;
            }
            else if (transform.position.x < 296.74f)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                movementDirection = Vector2.right * moveSpeed;  // Direção do movimento
            }
            else
            {
                transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
                movementDirection = Vector2.left * moveSpeed;  // Direção do movimento
            }
        }

        // Movimento vertical
        if (platform2)
        {
            if (transform.position.y > 3)
            {
                moveUp = false;
            }
            else if (transform.position.y < -1.64f)
            {
                moveUp = true;
            }

            if (moveUp)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                movementDirection = Vector2.up * moveSpeed;  // Direção do movimento
            }
            else
            {
                transform.Translate(Vector2.up * -moveSpeed * Time.deltaTime);
                movementDirection = Vector2.down * moveSpeed;  // Direção do movimento
            }
        }
    }

    // Função para pegar a direção do movimento da plataforma
    public Vector2 GetMovementDirection()
    {
        return movementDirection;
    }

    // Detectar o jogador em cima da plataforma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Torna o jogador filho da plataforma para segui-la
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Quando o jogador sai da plataforma, ele deixa de segui-la
            collision.transform.SetParent(null);
        }
    }
}
