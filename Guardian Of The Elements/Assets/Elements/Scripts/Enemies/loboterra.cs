using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loboterra : MonoBehaviour
{
     public float speed;
     public bool ground = true;
     public Transform groundCheck;
     public LayerMask groundLayer;
     public bool facingRight = true;
 
     // Start is called before the first frame update
     void Start()
     {
         
     }
 
     // Update is called once per frame
     void Update()
     {
         // Movimento contínuo do inimigo
         transform.Translate(Vector2.right * speed * Time.deltaTime);
 
         // Verificação de chão usando Linecast
         ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);
         Debug.Log(ground);
 
         // Se não estiver no chão, inverter direção
         if (!ground)
         {
             speed *= -1;
         }
 
         // Atualiza a orientação visual do inimigo
         if (speed > 0 && !facingRight)
         {
             Flip();
         }
         else if (speed < 0 && facingRight)
         {
             Flip();
         }
     }
 
     void Flip()
     {
         facingRight = !facingRight;
 
         // Inverter a escala do personagem para virar
         Vector3 scale = transform.localScale;
         scale.x *= -1;
         transform.localScale = scale;
 
         // Inverter a posição do groundCheck para alinhar com a nova direção
         Vector3 groundCheckPosition = groundCheck.localPosition;
         groundCheckPosition.x *= -1;
         groundCheck.localPosition = groundCheckPosition;
     }
 }