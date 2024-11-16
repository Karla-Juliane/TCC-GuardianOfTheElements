using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bloco : MonoBehaviour
{
    public GameObject blocoPrefab; // Referência ao bloco
    public Transform pontoA; // Ponto para onde o bloco sobe
    private bool isMoving = false; // Controla se o bloco está se movendo
    public float speedMove = 5f; // Velocidade de movimento
    public float timeToMoveUp = 3f; // Tempo para o bloco começar a subir após cair
    private bool isFalling = false; // Controla se o bloco está caindo
    private Rigidbody2D rb;

    private void Start()
    {
        rb = blocoPrefab.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        // Move o bloco de volta ao ponto A após o intervalo
        if (isMoving)
        {
            blocoPrefab.transform.position = Vector2.MoveTowards(
                blocoPrefab.transform.position,
                pontoA.position,
                speedMove * Time.deltaTime
            );

            // Para o movimento quando alcançar o ponto A
            if (Vector2.Distance(blocoPrefab.transform.position, pontoA.position) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isFalling)
        {
            // Faz o bloco cair
            isFalling = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 7;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("chao") && isFalling)
        {
            // Faz o bloco parar após atingir o chão
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0;

            // Inicia o temporizador para o bloco subir
            isFalling = false;
            Invoke(nameof(StartMovingUp), timeToMoveUp);
        }
    }

    private void StartMovingUp()
    {
        isMoving = true;
    }
}
