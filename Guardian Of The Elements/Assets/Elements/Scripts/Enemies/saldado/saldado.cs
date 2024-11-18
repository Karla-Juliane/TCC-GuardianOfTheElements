using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : MonoBehaviour
{
   
      private bool goRight;
          public int life;
          public float speed;
      
          // Variáveis para o movimento
          public float distanciaLimite = 5f; // Distância máxima que o inimigo pode percorrer antes de trocar de direção
          private float posicaoInicialX; // Guarda a posição inicial no eixo X
          private float posicaoInicialY; // Valor fixo do eixo Y
      
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
              goRight = true; // Inicializa indo para a direita
              posicaoInicialX = transform.position.x; // Guarda a posição inicial no eixo X
              posicaoInicialY = transform.position.y; // Guarda o valor fixo do eixo Y
              player = GameObject.FindGameObjectWithTag("Player").transform; // Encontra o jogador pela tag
              proximoAtaque = Time.time; // Inicializa o tempo do próximo ataque
          }
      
          // Update is called once per frame
          void Update()
          {
              MoverAutomaticamente();
              AtacarJogador();
          }
      
          void MoverAutomaticamente()
          {
              // Movimenta o inimigo para a direita ou esquerda apenas no eixo X
              float movimentoX = speed * Time.deltaTime * (goRight ? 1 : -1);
              transform.position = new Vector2(transform.position.x + movimentoX, posicaoInicialY);
      
              // Verifica se chegou ao limite de distância
              if (Mathf.Abs(transform.position.x - posicaoInicialX) >= distanciaLimite)
              {
                  goRight = !goRight; // Inverte a direção
                  transform.localScale = new Vector3(goRight ? 1f : -1f, 1f, 1f); // Ajusta o lado do sprite
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