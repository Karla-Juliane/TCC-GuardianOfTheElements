using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyS : MonoBehaviour
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
 
     [Header("Ataque")]
     public GameObject bulletPrefab; // Prefab do projétil
     public Transform firePoint; // Ponto de disparo do projétil
     public float bulletSpeed; // Velocidade do projétil
     public float attackCooldown; // Tempo entre os ataques
     private float cooldownTimer = 0f;
 
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
 
         // Atualizar cooldown
         if (cooldownTimer > 0)
             cooldownTimer -= Time.deltaTime;
 
         // Verificar a distância para atacar
         float distanceToPlayer = Vector2.Distance(transform.position, player.position);
         if (distanceToPlayer <= attackRange && cooldownTimer <= 0)
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
         isAttacking = true;
         cooldownTimer = attackCooldown;
 
         // Inicia a animação de ataque
         anim.SetBool("isAttacking", true);
 
         // Parar a movimentação enquanto ataca
         rig.velocity = Vector2.zero;
 
         // Disparar projétil
         Shoot();
 
         // Finaliza o ataque após o cooldown
         Invoke("StopAttack", 1f); // Ajuste o tempo conforme a duração da animação
     }
 
     void Shoot()
     {
         GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
         Vector2 direction = (player.position - firePoint.position).normalized;
         bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
     }
 
     void StopAttack()
     {
         isAttacking = false;
         anim.SetBool("isAttacking", false);
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