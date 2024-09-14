using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : MonoBehaviour
{
    private BoxCollider2D colliderEspectro;
    private bool goRight;
    public int life;
    public float speed;
    public Transform a;
    public Transform b;

    // Variáveis para o ataque
    public GameObject magiaPrefab; // Prefab da magia
    public Transform firePoint; // Ponto de onde a magia será lançada
    public float distanciaAtaque = 5f; // Distância mínima para atacar
    public float tempoEntreAtaques = 2f; // Tempo entre os disparos
    private float proximoAtaque;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        colliderEspectro = GetComponent<BoxCollider2D>();
        goRight = true; // Inicializa goRight para começar o movimento na direção correta
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encontra o jogador pela tag
        proximoAtaque = Time.time; // Inicializa o tempo do próximo ataque
    }

    // Update is called once per frame
    void Update()
    {
        MoverEntrePontos();
        AtacarJogador();
    }

    void MoverEntrePontos()
    {
        if (goRight)
        {
            // Mover para o ponto B
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);

            // Verifica se chegou perto do ponto B
            if (Vector2.Distance(transform.position, b.position) < 0.1f)
            {
                goRight = false; // Altera a direção para voltar ao ponto A
                transform.eulerAngles = new Vector3(0f, 180f, 0f); // Ajusta a rotação para a nova direção
            }
        }
        else
        {
            // Mover para o ponto A
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);

            // Verifica se chegou perto do ponto A
            if (Vector2.Distance(transform.position, a.position) < 0.1f)
            {
                goRight = true; // Altera a direção para ir ao ponto B
                transform.eulerAngles = new Vector3(0f, 0f, 0f); // Ajusta a rotação para a nova direção
            }
        }
    }

    void AtacarJogador()
    {
        // Verifica a distância entre o inimigo e o jogador
        if (Vector2.Distance(transform.position, player.position) <= distanciaAtaque && Time.time >= proximoAtaque)
        {
            // Dispara o projétil
            DispararMagia();
            proximoAtaque = Time.time + tempoEntreAtaques; // Atualiza o tempo do próximo ataque
        }
    }

    void DispararMagia()
    {
        // Instancia a magia na posição do ponto de disparo e na mesma rotação
        Instantiate(magiaPrefab, firePoint.position, firePoint.rotation);
    }
}
