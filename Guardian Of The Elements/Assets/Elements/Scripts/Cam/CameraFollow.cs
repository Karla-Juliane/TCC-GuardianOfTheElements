using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    [Header("Configurações de Suavização")]
    public float smooth = 0.125f;

    [Header("Offsets da Câmera")]
    public float heightOffset = 2f; // Offset da altura ajustável

    [Header("Limites da Câmera")]
    public float xMin = -48.4f; // Valor mínimo para a posição X
    public float xMax = 803.4f; // Valor máximo para a posição X
    public float yMin = -5f;    // Valor mínimo para a posição Y
    public float yMax = 5f;     // Valor máximo para a posição Y

    void Start()
    {
        // Encontra o jogador pelo Tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player não encontrado! Certifique-se de que o Player tem a tag 'Player'.");
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula a posição da câmera
            float targetX = Mathf.Clamp(player.position.x, xMin, xMax);
            float targetY = Mathf.Clamp(player.position.y + heightOffset, yMin, yMax);

            // Define a nova posição da câmera
            Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

            // Movimento suave da câmera
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
        }
    }
}