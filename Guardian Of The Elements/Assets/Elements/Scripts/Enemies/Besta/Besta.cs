using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Besta : MonoBehaviour
{
    [SerializeField] private Transform alvo; // Alvo a seguir
    [SerializeField] private float velocidadeMovimento; // Velocidade de movimento do inimigo
    [SerializeField] private float distanciaMinima; // Distância mínima para parar de se mover
    [SerializeField] private Rigidbody2D rigidbody; // Rigidbody para aplicar movimento
    [SerializeField] private SpriteRenderer spriteRenderer; // SpriteRenderer para inverter sprite
    [SerializeField] private float distanciaVisao; // Distância máxima de visão
    [SerializeField] private float anguloVisao; // Ângulo de visão em graus

    // Update is called uma vez por frame
    void Update()
    {
        if (AlvoEstaNoCampoDeVisao())
        {
            Vector2 posicaoAlvo = this.alvo.position;
            Vector2 posicaoAtual = this.transform.position;

            float distancia = Vector2.Distance(posicaoAtual, posicaoAlvo);

            if (distancia >= this.distanciaMinima)
            {
                Vector2 direcao = posicaoAlvo - posicaoAtual;
                direcao = direcao.normalized;

                this.rigidbody.velocity = (this.velocidadeMovimento * direcao);

                // Inverte o sprite baseado na direção
                if (this.rigidbody.velocity.x > 0)
                {
                    this.spriteRenderer.flipX = false;
                }
                else if (this.rigidbody.velocity.x < 0)
                {
                    this.spriteRenderer.flipX = true;
                }
            }
            else
            {
                this.rigidbody.velocity = Vector2.zero; // Para o movimento quando estiver próximo o suficiente
            }
        }
        else
        {
            this.rigidbody.velocity = Vector2.zero; // Se o alvo não estiver no campo de visão, não se move
        }
    }

    // Função para verificar se o alvo está dentro do campo de visão
    bool AlvoEstaNoCampoDeVisao()
    {
        Vector2 direcaoAlvo = this.alvo.position - this.transform.position;
        float distanciaAlvo = direcaoAlvo.magnitude;

        // Verifica se o alvo está dentro da distância de visão
        if (distanciaAlvo > this.distanciaVisao)
        {
            return false;
        }

        // Verifica se o alvo está dentro do ângulo de visão
        direcaoAlvo.Normalize();
        float anguloEntreInimigoEAlvo = Vector2.Angle(this.transform.right, direcaoAlvo);

        return anguloEntreInimigoEAlvo < this.anguloVisao / 2f;
    }
}
