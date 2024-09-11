using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspectroAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Demage(1);
        }
       
    }
}
