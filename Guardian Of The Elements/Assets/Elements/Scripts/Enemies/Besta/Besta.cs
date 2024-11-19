using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestaCorrompida : MonoBehaviour
{
    public float distancia; // Distância entre inimigo e jogador
    public float speed; // Velocidade do inimigo
    public Transform playerPos; // Transform do jogador
    public Rigidbody2D flyRb; // Rigidbody2D do inimigo
            
    void Start()
    {
        // Certifique-se de que todos os componentes estão configurados
        if (playerPos == null)
        {
            Debug.LogError("O Transform do jogador (playerPos) não foi atribuído.");
        }

        if (flyRb == null)
        {
            flyRb = GetComponent<Rigidbody2D>();
            if (flyRb == null)
            {
                Debug.LogError("Nenhum Rigidbody2D foi encontrado no inimigo.");
            }
        }
    }

    void Update()
    {
        distancia = Vector2.Distance(transform.position, playerPos.position);
        if (distancia < 12) // Caso esteja próximo o suficiente
        {
            Seguir();
        }
    }

    private void Seguir()
    {
        // Movimento direto sem física
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

        // Caso prefira usar o Rigidbody2D:
        /*
        Vector2 direction = (playerPos.position - transform.position).normalized;
        flyRb.velocity = direction * speed;
        */
    }
}