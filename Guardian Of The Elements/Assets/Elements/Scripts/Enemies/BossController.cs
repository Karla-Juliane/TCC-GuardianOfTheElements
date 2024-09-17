using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour
{
    public float distancia;
    public float baseSpeed = 3f;       // Velocidade inicial
    public float maxSpeed = 8f;        // Velocidade máxima permitida
    public float speedIncrease = 1.5f; // Aumento de velocidade a cada hit
    public float speedBoostDuration = 3f; // Duração do aumento de velocidade
    public Transform playerPos;
    public Rigidbody2D flyRb;

    private float currentSpeed;
    private bool isSpeedBoosted = false;

    void Start()
    {
        currentSpeed = baseSpeed; // Inicializa com a velocidade base
    }

    void Update()
    {
        // Calcula a distância até o jogador
        distancia = Vector2.Distance(transform.position, playerPos.position);
        
        // Segue o jogador se estiver dentro de uma certa distância
        if (distancia < 12)
        {
            Seguir();
        }
    }

    private void Seguir()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, currentSpeed * Time.deltaTime);
    }

    // Método para aumentar a velocidade quando o chefe for atingido
    public void TakeDamage()
    {
        // Aumenta a velocidade se ainda não atingiu a velocidade máxima
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncrease;
            currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed); // Garante que não ultrapasse a velocidade máxima
        }

        // Reinicia o boost se já estiver ativo, caso contrário, inicia o boost
        if (isSpeedBoosted)
        {
            CancelInvoke("ResetSpeed");
        }

        isSpeedBoosted = true;
        Invoke("ResetSpeed", speedBoostDuration); // Define o tempo que a velocidade ficará aumentada
    }

    // Método para restaurar a velocidade base após o tempo de boost
    private void ResetSpeed()
    {
        currentSpeed = baseSpeed; // Retorna à velocidade normal
        isSpeedBoosted = false; // Reseta o estado de boost
    }

    // Detecta a colisão com o tiro do jogador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o chefe foi atingido por um tiro
        if (collision.gameObject.CompareTag("PlayerController")) // Assumindo que o projétil do jogador tem a tag "PlayerBullet"
        {
            TakeDamage(); // Aumenta a velocidade ao levar um tiro

            // Destroi o tiro ao colidir com o chefe
            Destroy(collision.gameObject);
        }
    }
}