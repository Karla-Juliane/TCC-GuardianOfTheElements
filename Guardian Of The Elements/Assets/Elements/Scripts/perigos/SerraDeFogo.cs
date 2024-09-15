using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerraDeFogo : MonoBehaviour
{
    
   private float eixoZ;
   public GameObject pontoEsquerdo;
   public GameObject pontoDireito;
   private Vector2 novaPos;
   public float speed;




    void Start()
    {
        novaPos = pontoDireito.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GiroDaSerraDeFogo();
        MoverSerra();
    }

    private void GiroDaSerraDeFogo()
    {
        eixoZ = eixoZ + Time.deltaTime * 1000;
        transform.rotation = Quaternion.Euler(0, 0, eixoZ);
    }

    private void MoverSerra()
    {
        if(transform.position == pontoDireito.transform.position)
        {
            novaPos = pontoEsquerdo.transform.position;
        }

        if(transform.position == pontoEsquerdo.transform.position)
        {
            novaPos = pontoDireito.transform.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, novaPos, speed * Time.deltaTime);
    }
}
