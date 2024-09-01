using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspectroController : MonoBehaviour
{
    private BoxCollider2D colliderEspectro;
    private bool goRight;
    public int life;
    public float speed;

    public Transform a;

    public Transform b;
    // Start is called before the first frame update
    void Start()
    {
        colliderEspectro = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goRight == true)
        {
            if (Vector2.Distance(transform.position, b.position) < 0.1f)
            {
                goRight = false;
            }
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(transform.position, a.position) < 0.1f)
            {
                goRight = true;
            }
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.eulerAngles = new Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
        }
    }
}
