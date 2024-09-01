using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX;
    private BoxCollider2D colliderPlayer;

    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;
    public int life;
    public TextMeshProUGUI textLife;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderPlayer = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        textLife.text = life.ToString();
        
        if (life <= 0)
        {
            this.enabled = false;
            colliderPlayer.enabled = false;
            rb.gravityScale = 0;
            GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (isGrounded == true)
        {
            addJumps = 1;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && addJumps > 0)
            {
                addJumps--;
                Jump();
            }
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground");
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = collision.transform;
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground");
        {
            isGrounded = false;
        }
        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = null;
        }
    }
}
