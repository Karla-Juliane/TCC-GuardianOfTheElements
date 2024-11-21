using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
       public float speed; // Velocidade do inimigo
           public int life;
           public bool isDead;
           private Rigidbody2D rb;
           private BoxCollider2D colliderEnemy;
           private float timer; // Timer para controlar a troca de direção
           public float walkTime; // Tempo para trocar de direção
           private bool walkRight = true; // Controla a direção do inimigo
       
           private Animator anim; // Referência ao Animator
           private Rigidbody2D rig; // Referência ao Rigidbody2D
       
           public float attackRange; // Distância para começar a atacar
           public Transform player; // Referência ao jogador
           private bool isAttacking = false;
       
           void Start()
           {
               rb = GetComponent<Rigidbody2D>();
               colliderEnemy = GetComponent<BoxCollider2D>();
               isDead = false;
       
               rig = GetComponent<Rigidbody2D>();
               anim = GetComponent<Animator>();
           }
       
           void FixedUpdate()
           {
               if (isDead) return;
       
               // Verificar a distância para atacar
               float distanceToPlayer = Vector2.Distance(transform.position, player.position);
               if (distanceToPlayer <= attackRange && !isAttacking) // Inicia o ataque apenas se não estiver atacando
               {
                   Attack();
                   return;
               }
       
               // Movimentação do inimigo
               if (!isAttacking) // Só movimenta se não estiver atacando
               {
                   timer += Time.deltaTime;
       
                   if (timer >= walkTime)
                   {
                       walkRight = !walkRight;
                       timer = 0f;
                   }
       
                   if (walkRight)
                   {
                       transform.eulerAngles = new Vector2(0, 0);
                       rig.velocity = Vector2.right * speed;
                   }
                   else
                   {
                       transform.eulerAngles = new Vector2(0, 180);
                       rig.velocity = Vector2.left * speed;
                   }
               }
           }
       
           public void Attack()
           {
               if (!isAttacking)
               {
                   isAttacking = true;
                   anim.SetBool("isAttacking", true); // Inicia a animação de ataque
                   rig.velocity = Vector2.zero; // Parar a movimentação durante o ataque
       
                   Debug.Log("Ataque iniciado!");
       
                   // Reseta o ataque após a animação (ajuste o tempo conforme necessário)
                   Invoke("StopAttack", 1f); // O tempo de 1 segundo deve corresponder ao tempo da animação de ataque
               }
           }
       
           void StopAttack()
           {
               isAttacking = false;
               anim.SetBool("isAttacking", false); // Finaliza a animação de ataque
               Debug.Log("Ataque finalizado!");
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
                       float knockbackForce = 10f; // Ajuste conforme necessário
                       Vector2 knockback = knockbackDirection * knockbackForce;
                       
                       player.ApplyKnockback(knockback);
                       Debug.Log("Knockback aplicado ao jogador!");
                   }
               }
               
               if (col.gameObject.CompareTag("Bola_terra"))
               {
                   Debug.Log("Acertou poder da terra");
                   Demage(1);
               }
               if (col.gameObject.CompareTag("Bola_fogo"))
               {
                   Demage(1);
               }
               if (col.gameObject.CompareTag("Bola_agua"))
               {
                   Demage(1);
               }
               if (col.gameObject.CompareTag("Bola_vento"))
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