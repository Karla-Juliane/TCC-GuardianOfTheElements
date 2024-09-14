using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magia : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade do projétil
    public float tempoDeVida = 2f; // Quanto tempo o projétil dura antes de ser destruído

    void Start()
    {
        // Destroi o projétil após um tempo para evitar que ele fique indefinidamente na cena
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        // Mover o projétil para frente (na direção em que ele foi disparado)
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Aqui você pode adicionar o que acontece quando a magia atinge algo
        if (col.CompareTag("Player"))
        {
            // Lógica de dano ao jogador, etc.
            Destroy(gameObject); // Destrói a magia quando colide com o jogador
        }
    }
}
