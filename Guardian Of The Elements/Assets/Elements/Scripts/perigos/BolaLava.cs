using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaLava : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float Velocidade;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            BolaFogo();
        }
    }
    void BolaFogo()
    {
        rb2d.velocity = transform.up * Velocidade;
    }


}
