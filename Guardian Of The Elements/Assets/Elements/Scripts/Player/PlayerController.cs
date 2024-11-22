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
    
    public bool isAttacking = false;
    
    public Transform firePoint; // Ponto de onde o ataque será lançada
    public Transform bolaAguaPoint; // Ponto de onde o ataque de agua será lançado
    public Transform bolaVentoPoint;
    public Transform bolaFogoPoint;
    
    private bool isKnockedBack = false; // Controla se o jogador está em knockback
    private float knockbackDuration = 0.5f; // Duração do knockback
        
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        novaPorta = GameObject.Find("novaPorta");
        
        levelName = SceneManager.GetActiveScene().name;
        
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
        if (isKnockedBack) return; // Ignora a movimentação durante o knockback
        
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        // Se o jogador está se movendo para a direita
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetInteger("transition", 1);;
        }
        // Se o jogador está se movendo para a esquerda
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetInteger("transition", 1);;
        }
        // Se o jogador não está se movendo
        else if (isGrounded && !isAttacking)
        {
            anim.SetInteger("transition", 0);; // Para a animação de corrida
        }
    }

    void Jump()
    {
        anim.SetTrigger("jump");  // Define o trigger para pulo
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        ParticleObserver.OnParticleSpawnEvent(transform.position);
    }


    public void Demage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            anim.SetInteger("transition", 6);
            this.enabled = false;
            //colliderPlayer.enabled = false;
            rb.Sleep();
            
           // GetComponent<SpriteRenderer>().color = Color.black;
           //Mandar info que o jogador morreu para dentro do animator
           //anim.SetTrigger("die");
           //Destroy(gameObject, 0.8f);
           
           if (GameManager.instance != null)
           {
               GameManager.instance.GameOver();
           }
        }
    }
    
    public void RestartLevel(string levelName) 
    {
        GameManager.instance.CarregarDepoisDe(levelName, 0.1f);
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("isGrounded", isGrounded);
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
    
    private IEnumerator ResetIsAttacking()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo
        isAttacking = false;
    }
    
    public void LaunchBolaTerra()
    {
        Instantiate(bolaTerraPrefab, firePoint.position, firePoint.rotation);
    }
    
    public void LaunchBolaAgua()
    {
        Instantiate(bolaAguaPrefab, bolaAguaPoint.position, bolaAguaPoint.rotation);
    }
    
    public void LaunchBolaVento()
    {
        Instantiate(bolaVentoPrefab, bolaVentoPoint.position, bolaVentoPoint.rotation);
    }
    
    public void LaunchBolaFogo()
        {
            Instantiate(bolaFogoPrefab, bolaFogoPoint.position, bolaFogoPoint.rotation);
        }

    private void Atacar() 
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
           isAttacking = true;
           anim.SetInteger("transition", 2); // Inicia a animação
           StartCoroutine(ResetIsAttacking()); // Reseta após o tempo necessário
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if(currentScene.buildIndex < 3)
            {
                return;
            }
            
           isAttacking = true;
           anim.SetInteger("transition", 3); // Inicia a animação
           StartCoroutine(ResetIsAttacking()); // Reseta após o tempo necessário
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if(currentScene.buildIndex < 4)
            {
                return;
            }
            
           isAttacking = true;
           anim.SetInteger("transition", 4); // Inicia a animação
           StartCoroutine(ResetIsAttacking()); // Reseta após o tempo necessário
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if(currentScene.buildIndex < 5)
            {
                return;
            }
            
          isAttacking = true;
          anim.SetInteger("transition", 5); // Inicia a animação
          StartCoroutine(ResetIsAttacking()); // Reseta após o tempo necessário
        }
    }
    
    public void ApplyKnockback(Vector2 force)
    {
        if (isKnockedBack) return; // Evita múltiplos knockbacks
    
        isKnockedBack = true;
        rb.velocity = Vector2.zero; // Reseta qualquer movimento atual
        rb.AddForce(force, ForceMode2D.Impulse); // Aplica a força de knockback
    
        Invoke(nameof(ResetKnockback), knockbackDuration); // Reseta o knockback após a duração
    }
    
    private void ResetKnockback()
    {
        isKnockedBack = false;
    }
}