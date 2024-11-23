using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour

{
   
    private Transform player;
    public float smooth = 0.125f;
    public float heightOffset = 2f; // Offset da altura ajustável

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player.position.x >= -20 && player.position.x < 345)
        {
            // Adiciona o offset de altura
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y + heightOffset, transform.position.z);
            // Movimento suave da câmera
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
        }
    }
}