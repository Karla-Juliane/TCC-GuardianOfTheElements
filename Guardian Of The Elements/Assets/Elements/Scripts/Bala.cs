using UnityEngine;

public class Bala : MonoBehaviour
{
    
    public int damage = 1; // Dano da bala

    // Função chamada quando a bala colide com algo
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Verifica se a bala atingiu o jogador
        if (col.gameObject.CompareTag("Player"))
        {
            // Aplica o dano ao jogador
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Demage(damage); // Chama o método de dano no jogador
                Debug.Log("Bala atingiu o jogador!");
            }

            // Destruir a bala após a colisão
            Destroy(gameObject);
        }
        else if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Wall"))
        {
            // A bala colide com inimigos ou paredes e é destruída
            Destroy(gameObject);
        }
    }
}