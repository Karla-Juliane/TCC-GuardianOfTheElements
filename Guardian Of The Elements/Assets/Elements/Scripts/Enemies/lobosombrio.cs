using System.Collections;
using System.Collections.Generic;
using Elements.Scripts.Player;
using UnityEngine;

public class lobosombrio : MonoBehaviour
{
    public float walkTime; // Tempo para trocar de direção
    private bool walkRight = true; // Controla a direção do inimigo

    public int damage = 1; // Dano causado ao jogador

    private Animator anim; // Referência ao Animator
    private Rigidbody2D rig; // Referência ao Rigidbody2D

    // Start é chamado antes do primeiro frame
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Verifica se os componentes necessários estão conectados
        if (rig == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no inimigo!");
        }
        if (anim == null)
        {
            Debug.LogError("Animator não encontrado no inimigo!");
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Demage(damage);
            }
            else
            {
                Debug.LogError("PlayerController não encontrado no jogador!");
            }
        }
    }
}
    