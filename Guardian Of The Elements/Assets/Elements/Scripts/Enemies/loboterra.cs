using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loboterra : MonoBehaviour
{
public float speed;
    public bool ground = true;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        // Inicialização, se necessário
    }

    // Update is called once per frame
    void Update()
    {
        // Verificação de chão usando Raycast (detectando abaixo do inimigo)
        RaycastHit2D hit;
        if (facingRight)
        {
            // Raycast à frente do inimigo para detectar o chão (movendo para a direita)
            hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);
        }
        else
        {
            // Raycast à frente do inimigo para detectar o chão (movendo para a esquerda)
            hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);
        }

        // Se não houver chão, invertemos a direção
        ground = hit.collider != null;

        Debug.Log("No Ground: " + !ground);

        // Se não estiver no chão, inverter direção
        if (!ground)
        {
            speed *= -1; // Inverte a direção
            flip();      // Chama o flip para inverter a orientação visual
        }

        // Movimento lateral (direção depende de facingRight)
        if (facingRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    // Método para inverter a direção do inimigo
    void flip()
    {
        facingRight = !facingRight;

        // Inverter a escala do personagem para virar
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}