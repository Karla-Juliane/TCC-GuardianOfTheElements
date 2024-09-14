using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestaCorrompida : MonoBehaviour
{
    public float distancia;
    public float speed;
    public Transform playerPos;
    public Rigidbody2D flyRb;
            
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        distancia = Vector2.Distance(transform.position, playerPos.position);
        if (distancia < 12)
        {
            Seguir();
        }
    }

    private void Seguir()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }
}
