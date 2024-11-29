using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueTerra : MonoBehaviour
{
    private Rigidbody2D rig;
    public float velocidade = 5f; // Velocidade do projétil
    public float tempoDeVida = 2f; // Quanto tempo o projétil dura antes de ser destruído
    // Start is called before the first frame update
    void Start()
    {
         Destroy(gameObject, tempoDeVida);
         rig = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D colision)
    {
        if(colision.gameObject.tag == "Enemie")
        {
            //colision.gameObject.GetComponent<Enemy>().Death();
            Destroy(gameObject);
        }
    }
}
