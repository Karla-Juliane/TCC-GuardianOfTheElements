using Elements.Scripts.Player;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public int damage = 1; // Dano da bala
    public float speed = 5f; // Velocidade da bala
    public float followDuration = 3f; // Tempo que a bala segue o jogador
    private Transform target; // Alvo para seguir
    private Rigidbody2D rb; // Referência ao Rigidbody2D
    private bool isFollowing = false; // Indica se está seguindo o jogador

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform player)
    {
        target = player;
        isFollowing = true;
        Invoke(nameof(StopFollowing), followDuration); // Para de seguir após followDuration
    }

    void Update()
    {
        if (isFollowing && target != null)
        {
            // Calcula a direção para o jogador
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    private void StopFollowing()
    {
        isFollowing = false;
        rb.velocity = Vector2.zero; // Para o movimento
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Demage(damage);
            }
            Destroy(gameObject);
        }
        /*else if (col.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }*/
    }
}