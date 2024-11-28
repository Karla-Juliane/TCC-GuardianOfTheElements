using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cristal : MonoBehaviour
{
    private void OTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            print("voce coletou um item");
        }
    }
}
