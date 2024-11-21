using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s : MonoBehaviour
{
     public float speed = 2f; // Velocidade do inimigo
       public float distance = 1f; // Distância do Raycast
       private bool isRight = true; // Direção inicial
       public Transform groundCheck; // Ponto para verificar o chão
   
       void Update()
       {
           // Movimenta o inimigo para frente
           transform.Translate(Vector2.right * speed * Time.deltaTime);
   
           // Raycast para verificar o chão
           RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
   
           // Se não detectar chão, inverte a direção
           if (ground.collider == null)
           {
               Flip();
           }
       }
   
       // Método para alternar a direção
       void Flip()
       {
           isRight = !isRight; // Alterna a direção
           Vector3 scaler = transform.localScale;
           scaler.x *= -1; // Inverte o eixo X
           transform.localScale = scaler; // Aplica a mudança
       }
   
       // Visualização no editor do Raycast
       void OnDrawGizmos()
       {
           Gizmos.color = Color.red;
           Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * distance);
       }
       
   }    