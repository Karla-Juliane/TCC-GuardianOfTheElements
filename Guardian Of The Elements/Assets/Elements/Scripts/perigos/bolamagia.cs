using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolamagia : MonoBehaviour
{
    public GameObject player;
    
    private Rigidbody2D rb;
   
    public float speed = 1f;
    
    private void Start()
    {
     
        player = GameObject.Find("HeadPlayer");;
    }
    
    private void FixedUpdate()
    {
         transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        speed = 0; 
        Destroy(gameObject.GetComponent<CircleCollider2D>());
          Destroy(gameObject.GetComponent<Rigidbody2D>());
          Destroy(gameObject, 0.4f);
    }
    
}

