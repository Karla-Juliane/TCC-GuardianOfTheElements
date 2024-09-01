using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspectroController : MonoBehaviour
{
    private BoxCollider2D colliderEspectro;
    private bool goRight;
    public int life;
    public float speed;

    public Transform a;
    public Transform b;

    // Start is called before the first frame update
    void Start()
    {
        colliderEspectro = GetComponent<BoxCollider2D>();
        goRight = true; // Inicializa goRight para começar o movimento na direção correta
    }

    // Update is called once per frame
    void Update()
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
}