using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    // Start is called before the first frame update
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
    
    public void Demage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            this.enabled = false;
            colliderEnemy.enabled = false;
            rb.gravityScale = 0;
            anim.SetBool("death", true);
            speed = 0f;
            Destroy(gameObject, 1f);
        }
    }
    
     private void OnTriggerEnter2D(Collider2D col)
     {
        if(col.gameObject.CompareTag("Bola_terra")) {
            Debug.Log("Acertou poder da terra");
            Demage(1);
        }
        if(col.gameObject.CompareTag("Bola_fogo"))
        {
            Demage(1);
        }
        if(col.gameObject.CompareTag("Bola_agua"))
        {
            Demage(1);
        }
        if(col.gameObject.CompareTag("Bola_vento"))
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
