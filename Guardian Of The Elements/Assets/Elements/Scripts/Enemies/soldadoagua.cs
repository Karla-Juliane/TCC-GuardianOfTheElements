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
    public float attackCooldown = 3f; // Intervalo entre ataques

    private bool isAttacking = false; // Se o inimigo está atacando
    private bool canAttack = true; // Controle para o intervalo de ataque
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
        if (distanceToPlayer <= attackRange && canAttack) // Se o jogador estiver dentro do alcance e pode atacar
        {
            Attack();
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
            canAttack = false; // Bloqueia ataques até que o cooldown termine
            anim.SetBool("isAttacking", true); // Inicia a animação de ataque
            rb.velocity = Vector2.zero; // Parar a movimentação durante o ataque

            Debug.Log("Ataque iniciado!");

            // Atirar durante o ataque
            Atirar();

            // Reseta o ataque após a animação
            Invoke("StopAttack", 1f);

            // Reativa a possibilidade de atacar após o cooldown
            Invoke("ResetAttackCooldown", attackCooldown);
        }
    }

    void StopAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", false);
        Debug.Log("Ataque finalizado!");
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
        Debug.Log("Cooldown de ataque finalizado. Pronto para atacar novamente.");
    }

    private void Atirar()
    {
        // Criar o projétil
        GameObject temp = Instantiate(balaProjetil, arma.position, arma.rotation);

        // Configurar a bala para seguir o jogador
        Bala balaScript = temp.GetComponent<Bala>();
        if (balaScript != null)
        {
            balaScript.SetTarget(player);
        }

        // Destruir o projétil após 5 segundos
        Destroy(temp, 5f);
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

            player.kbCount = player.kbTime;
            if (col.transform.position.x <= transform.position.x)
            {
                player.isKnockRight = true;
            }
            if (col.transform.position.x > transform.position.x)
            {
                player.isKnockRight = false;
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