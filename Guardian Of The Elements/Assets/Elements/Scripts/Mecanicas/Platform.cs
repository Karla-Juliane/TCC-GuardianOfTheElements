using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float moveSpeed;
    public bool useTransform;
    public bool shouldFlip;

    [SerializeField] private Vector2 movePosition;
    [SerializeField] private Transform moveDestination;

    private Vector2 initialPosition;
    private Vector2 moveTarget;
    private bool isReturning;

    private float originalLocalScaleX;

    void Start()
    {
        if (shouldFlip) originalLocalScaleX = transform.localScale.x;

        if (useTransform)
        {
            moveTarget = moveDestination.position; // Usar a posição mundial do Transform
        }
        else
        {
            moveTarget = initialPosition + movePosition;
        }

        initialPosition = transform.position;
    }

    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        // Determina o destino atual (avançando ou voltando)
        Vector2 targetPosition = isReturning ? initialPosition : moveTarget;

        // Calcula o passo de movimento para este quadro
        float step = moveSpeed * Time.deltaTime;

        // Move a plataforma diretamente para o destino com limite
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

        // Verifica se a plataforma chegou ao destino
        if (Vector2.Distance(transform.position, targetPosition) <= 0.01f)
        {
            isReturning = !isReturning; // Inverte o movimento

            // Controla o flip, se necessário
            if (shouldFlip)
            {
                transform.localScale = new Vector3(
                    isReturning ? -originalLocalScaleX : originalLocalScaleX,
                    transform.localScale.y,
                    transform.localScale.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Verifica se o objeto atual ainda está ativo antes de alterar o parent
            if (gameObject.activeInHierarchy)
            {
                other.transform.SetParent(null);
            }
        }
    }


    private void OnDrawGizmos()
    {
        // Desenha as linhas de movimento no editor para depuração
        Vector3 destination = useTransform && moveDestination != null
            ? moveDestination.position
            : (Vector3)(initialPosition + movePosition);

        Debug.DrawLine(transform.position, destination, Color.green);
    }
}
