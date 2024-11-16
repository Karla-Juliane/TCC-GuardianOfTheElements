using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private bool porta;
    private GameObject novaPorta;
    
    private Rigidbody2D rb;
    private float moveX;
    private BoxCollider2D colliderPlayer;
    
    private Scene currentScene;

    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;
    public int life;
    public TextMeshProUGUI textLife;
    public string levelName;

    public Animator anim; 

    public GameObject bolaTerraPrefab; // Prefab do ataque de terra
    public GameObject bolaAguaPrefab;
    public GameObject bolaVentoPrefab;
    public GameObject bolaFogoPrefab;
    
    public Transform firePoint; // Ponto de onde o ataque será lançada
    
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        novaPorta = GameObject.Find("novaPorta");
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colliderPlayer = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        novaPosicao();
        moveX = Input.GetAxisRaw("Horizontal");
        textLife.text = life.ToString();
        Atacar();
        


        Move();

        if (isGrounded == true)
        {
            addJumps = 1;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
                AudioObserver.OnPlaySfxEvent("pulo");
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && addJumps > 0)
            {
                addJumps--;
                Jump();
                AudioObserver.OnPlaySfxEvent("pulo");
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
        ParticleObserver.OnParticleSpawnEvent(transform.position);
    }

    public void Demage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            this.enabled = false;
            colliderPlayer.enabled = false;
            rb.Sleep();
           // GetComponent<SpriteRenderer>().color = Color.black;
           //Mandar info que o jogador morreu para dentro do animator
           anim.SetTrigger("die");
           //Destroy(gameObject, 0.8f);
           
           
           
           GameManager.instance.CarregarDepoisDe(levelName,2);
        }
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground");
        {
            isGrounded = true;
            anim.SetBool("isGrounded",isGrounded);
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
            anim.SetBool("isGrounded",isGrounded);
        }
        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = null;
        }
    }

    private void novaPosicao()
    {
        if (porta == true)
        {
            rb.transform.position = new Vector2(novaPorta.transform.position.x, novaPorta.transform.position.y);
            porta = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("next"))
        {
            porta = true;
        }

        if(col.gameObject.CompareTag("Enemie"))
        {
            Demage(1);
        }

        if(col.gameObject.CompareTag("Magia")) {
            Demage(1);
        }
        if(col.gameObject.CompareTag("serra"))
        {
            Demage(1);
        }
        if(col.gameObject.CompareTag("pendulo"))
                {
                    Demage(5);
                }
         if(col.gameObject.CompareTag("armadilha"))
                        {
                            Demage(1);
                        }
         if(col.gameObject.CompareTag("bloco"))
         {
            Demage(5);
         }
    }

    private void Atacar() 
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(bolaTerraPrefab, firePoint.position, firePoint.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if(currentScene.buildIndex < 1)
            {
                return;
            }
            
            Instantiate(bolaAguaPrefab, firePoint.position, firePoint.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if(currentScene.buildIndex < 2)
            {
                return;
            }
            
            Instantiate(bolaVentoPrefab, firePoint.position, firePoint.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if(currentScene.buildIndex < 3)
            {
                return;
            }
            
            Instantiate(bolaFogoPrefab, firePoint.position, firePoint.rotation);
        }
    }
}