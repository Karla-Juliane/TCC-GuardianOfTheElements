using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : MonoBehaviour
{
   
      private bool goRight;
          public int life;
          public float speed;
      
          //
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
            
              
              player = GameObject.FindGameObjectWithTag("Player").transform; // Encontra o jogador pela tag
              proximoAtaque = Time.time; // Inicializa o tempo do próximo ataque
          }
      
          // Update is called once per frame
          void Update()
          {
             
              AtacarJogador();
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