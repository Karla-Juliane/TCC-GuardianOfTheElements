using System.Collections;
using System.Collections.Generic;
using Elements.Scripts.Player;
using Unity.Collections;
using UnityEngine;

public class soldadoagua : MonoBehaviour
{ 
    
     public GameObject balaProjetil; // Prefab da bala
    public Transform arma; // Posição da arma para spawn da bala
    public float forcaDoTiro = 5f; // Velocidade da bala
    public float attackRange = 5f; // Distância para começar a atacar
    public Transform player; // Referência ao jogador
    public float speed; // Velocidade do inimigo
    public int life; // Vida do inimigo
    public bool isDead; // Verifica se o inimigo morreu
    public float walkTime = 2f; // Tempo para trocar de direção

    private bool isAttacking = false; // Se o inimigo está atacando
    private bool walkRight = true; // Controla a direção do inimigo
    private float timer; // Timer para controlar a troca de direção
    private Rigidbody2D rb; // Rigidbody do inimigo
    private BoxCollider2D colliderEnemy; // Collider do inimigo
    private Animator anim; // Referência ao Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderEnemy = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        isDead = false;
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // Verificar a distância entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && !isAttacking) // Se o jogador estiver dentro do alcance de ataque
        {
            Attack(); // Inicia o ataque (tiro)
        }

        // Movimentação do inimigo
        if (!isAttacking)
        {
            timer += Time.deltaTime;

            if (timer >= walkTime)
            {
                walkRight = !walkRight;
                timer = 0f;
            }

            if (walkRight)
            {
                transform.eulerAngles = new Vector2(0, 0); // Olha para a direita
                rb.velocity = Vector2.right * speed;
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180); // Olha para a esquerda
                rb.velocity = Vector2.left * speed;
            }
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true); // Inicia a animação de ataque
            rb.velocity = Vector2.zero; // Parar a movimentação durante o ataque

            Debug.Log("Ataque iniciado!");

            // Atirar durante o ataque
            Atirar();

            // Reseta o ataque após a animação (ajuste o tempo conforme necessário)
            Invoke("StopAttack", 1f);
        }
    }

    void StopAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", false);
        Debug.Log("Ataque finalizado!");
    }

    private void Atirar()
    {
        // Criar o projétil
        GameObject temp = Instantiate(balaProjetil, arma.position, arma.rotation);

        // Configurar a direção da bala baseada na rotação do inimigo
        Rigidbody2D rbBala = temp.GetComponent<Rigidbody2D>();
        if (rbBala != null)
        {
            // A direção depende do lado que o inimigo está olhando
            Vector2 direcao = transform.eulerAngles.y == 0 ? Vector2.right : Vector2.left;
            rbBala.velocity = direcao * forcaDoTiro;
        }

        // Destruir o projétil após 3 segundos
        Destroy(temp, 3f);
    }

    public void Demage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                Vector2 knockbackDirection = (col.transform.position - transform.position).normalized;
                float knockbackForce = 10f;
                Vector2 knockback = knockbackDirection * knockbackForce;

                player.ApplyKnockback(knockback);
                Debug.Log("Knockback aplicado ao jogador!");
            }
        }

        // Verifica colisões com poderes do jogador
        if (col.gameObject.CompareTag("Bola_terra") || col.gameObject.CompareTag("Bola_fogo") || 
            col.gameObject.CompareTag("Bola_agua") || col.gameObject.CompareTag("Bola_vento"))
        {
            Demage(1);
        }
    }

    public void Death()
    {
        isDead = true;
        anim.SetBool("death", true);
        speed = 0f;
        colliderEnemy.enabled = false;
        rb.gravityScale = 0;
        Destroy(gameObject, 1f);
    }
}