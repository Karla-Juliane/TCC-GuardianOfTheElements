using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public float smooth = 0.125f;
    public float heightOffset = 2f; // Offset da altura ajustável

    // Limites para a posição vertical da câmera
    public float yMin = -5f; // Valor mínimo para a posição y
    public float yMax = 5f;  // Valor máximo para a posição y

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player.position.x >= -48.4 && player.position.x < 803.4)
        {
            // Calcula a posição da câmera com o offset de altura
            float targetY = player.position.y + heightOffset;

            // Restringe a posição y dentro do intervalo definido
            targetY = Mathf.Clamp(targetY, yMin, yMax);

            // Cria a nova posição da câmera, mantendo a posição x do jogador e a posição y restrita
            Vector3 targetPosition = new Vector3(player.position.x, targetY, transform.position.z);

            // Movimento suave da câmera
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
        }
    }
}