using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int life;
    public bool isDead;
    private Rigidbody2D rb;
    private BoxCollider2D colliderEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderEnemy = GetComponent<BoxCollider2D>();
        isDead = false;
    }
    
    public void Demage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            this.enabled = false;
            colliderEnemy.enabled = false;
            rb.gravityScale = 0;
            Destroy(gameObject);
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
}
