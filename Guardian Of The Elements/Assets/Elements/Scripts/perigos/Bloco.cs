using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    public GameObject blocoPrefab;
    public GameManager pontoA;
    private bool isMove;
    public float speedMove;

    // Update is called once per frame
    void Update()
    {
         if(isMove == true)
         {
            blocoPrefab.transform.position = Vector2.MoveTowards(blocoPrefab.transform.position, pontoA.transform.position,speedMove * Time.deltaTime);
         }
    }

private void OnTriggerEnter2D(Collider2D col)
{
    if(col.gameObject.CompareTag("Player"))
    {
        blocoPrefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
       blocoPrefab.GetComponent<Rigidbody2D>().gravityScale = 7;
       blocoPrefab.GetComponent<Rigidbody2D>().mass = 400;

    }
}


private void OnCollisionEnter2D(Collision2D col)
{
  if(col.gameObject.CompareTag("chao"))
  {
    blocoPrefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    blocoPrefab.GetComponent<Rigidbody2D>().gravityScale = 0;
    blocoPrefab.GetComponent<Rigidbody2D>().mass = 0;

   isMove = true;
  }


}

}
