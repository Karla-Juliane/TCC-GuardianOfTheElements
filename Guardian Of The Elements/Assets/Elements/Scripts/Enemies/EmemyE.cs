using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyE : MonoBehaviour
{

   public float speed; // Velocidade de patrulha
      public float chaseSpeed; // Velocidade de perseguição
      public int life;
      public bool isDead;
      public float detectionRange; // Distância para detectar o jogador
      public Transform player; // Referência ao Transform do jogador
   
   
      private Rigidbody2D rb;
      private BoxCollider2D colliderEnemy;
      private float timer; // Timer para controlar a troca de direção
      public float walkTime; // Tempo para trocar de direção
      private bool walkRight = true; // Controla a direção do inimigo
   
   
      private Animator anim; // Referência ao Animator
      private Rigidbody2D rig; // Referência ao Rigidbody2D
   
   
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
   
   
      void FollowPlayer()
      {
          Vector2 direction = (player.position - transform.position).normalized;
          rig.velocity = new Vector2(direction.x * chaseSpeed, rig.velocity.y);
   
   
          if (player.position.x < transform.position.x)
          {
              transform.eulerAngles = new Vector2(0, 180);
          }
          else
          {
              transform.eulerAngles = new Vector2(0, 0);
          }
      }
   
   
      public void Demage(int damage)
      {
          life -= damage;
          if (life <= 0)
          {
              isDead = true;
              colliderEnemy.enabled = false;
              rb.gravityScale = 0;
              anim.SetBool("death", true);
              speed = 0f;
              rig.velocity = Vector2.zero; // Para parar o movimento
              Destroy(gameObject, 1f);
          }
      }
   
   
      private void OnTriggerEnter2D(Collider2D col)
      {
          if (col.gameObject.CompareTag("Bola_terra") ||
              col.gameObject.CompareTag("Bola_fogo") ||
              col.gameObject.CompareTag("Bola_agua") ||
              col.gameObject.CompareTag("Bola_vento"))
          {
              Demage(1);
          }
      }
   
   
      public void Death()
      {
          anim.SetBool("death", true);
          speed = 0f;
      }
   }
   
