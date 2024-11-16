using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
     public GameObject spikeBlockObject;
     // Start is called before the first frame update
     void Start()
     {
         
     }
 
     // Update is called once per frame
     void Update()
     {
         
     }
 
     void OnTriggerEnter2D(Collider2D collisionInfo)
     {
         if(collisionInfo.gameObject.CompareTag("Player")){
             spikeBlockObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
             spikeBlockObject.GetComponent<Rigidbody2D>().gravityScale = 7;
             spikeBlockObject.GetComponent<Rigidbody2D>().mass = 400;
         }
     }
 }