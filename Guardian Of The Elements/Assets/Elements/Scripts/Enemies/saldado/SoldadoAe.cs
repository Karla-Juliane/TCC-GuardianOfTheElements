using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldadoAe : MonoBehaviour
{
public float speed;
public float walkTime;
private bool walkRight = true;

public int damage = 1;

private float timer;

private Animator anim;
private Rigidbody2D rig;
// Start is called before the first frame update
void Start()
{
    rig = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
}

// Update is called once per frame
void Update()
{
        
}

void FixedUpdate()
{
    timer += Time.deltaTime;

    if(timer >= walkTime)
    {
        walkRight = !walkRight;
        timer = 0f;
    }

    if(walkRight)
    {
        transform.eulerAngles = new Vector2(0, 0);
        rig.velocity = Vector2.right * speed;
    }
    else
    {
        transform.eulerAngles = new Vector2(0, 180);
        rig.velocity = Vector2.left * speed;
    }
}

public void Death()
{
    anim.SetBool("death", true);
    speed = 0f;
    Destroy(gameObject, 1f);
}

private void OnCollisionEnter2D(Collision2D collision)
{
    if(collision.gameObject.tag == "Player")
    {
        collision.gameObject.GetComponent<PlayerController>().Demage(damage);
    }
}

}
