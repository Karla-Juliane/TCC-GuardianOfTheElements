using System.Collections;
using System.Collections.Generic;
using Elements.Scripts.Player;
using UnityEngine;

public class boss : MonoBehaviour
{ public float speed; // Velocidade de patrulha
     public float chaseSpeed; // Velocidade de perseguição
     public int life; // Vida do inimigo
     public bool isDead; // Status de morte
     public float detectionRange; // Distância para detectar o jogador
     public Transform player; // Referência ao Transform do jogador
 
     private Rigidbody2D rb;
     private BoxCollider2D colliderEnemy;
     private float timer; // Timer para controlar a troca de direção
     public float walkTime; // Tempo para trocar de direção
     private bool walkRight = true; // Controla a direção do inimigo
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
 
         float distanceToPlayer = Vector2.Distance(transform.position, player.position);
 
         if (distanceToPlayer <= detectionRange)
         {
             FollowPlayer();
         }
         else
         {
             Patrol();
         }
     }
 
     void Patrol()
     {
         anim.SetBool("isChasing", false); // Desativa animação de perseguição
         timer += Time.deltaTime;
 
         if (timer >= walkTime)
         {
             walkRight = !walkRight;
             timer = 0f;
         }
 
         if (walkRight)
         {
             transform.eulerAngles = new Vector2(0, 0);
             rb.velocity = Vector2.right * speed;
         }
         else
         {
             transform.eulerAngles = new Vector2(0, 180);
             rb.velocity = Vector2.left * speed;
         }
     }
 
     void FollowPlayer()
     {
         anim.SetBool("isChasing", true); // Ativa animação de perseguição
         Vector2 direction = (player.position - transform.position).normalized;
         rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
 
         // Ajusta a direção do inimigo
         if (player.position.x < transform.position.x)
         {
             transform.eulerAngles = new Vector2(0, 180);
         }
         else
         {
             transform.eulerAngles = new Vector2(0, 0);
         }
     }
 
     public void Damage(int damage)
     {
         life -= damage;
         if (life <= 0)
         {
             Die();
         }
     }
 
     private void Die()
     {
         isDead = true;
         colliderEnemy.enabled = false;
         rb.gravityScale = 0;
         anim.SetBool("death", true);
         rb.velocity = Vector2.zero; // Para movimento
         Destroy(gameObject, 1f); // Destroi após 1 segundo
     }
 
     private void OnTriggerEnter2D(Collider2D col)
     {
         if (col.gameObject.CompareTag("Player"))
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                Vector2 knockbackDirection = (col.transform.position - transform.position).normalized;
                float knockbackForce = 20f; // Ajuste conforme necessário
                Vector2 knockback = knockbackDirection * knockbackForce;
                
                player.ApplyKnockback(knockback);
                Debug.Log("Knockback aplicado ao jogador!");
            }
        }
         
         if (col.gameObject.CompareTag("Bola_terra") ||
             col.gameObject.CompareTag("Bola_fogo") ||
             col.gameObject.CompareTag("Bola_agua") ||
             col.gameObject.CompareTag("Bola_vento"))
         {
             Damage(1);
         }
     }
 }