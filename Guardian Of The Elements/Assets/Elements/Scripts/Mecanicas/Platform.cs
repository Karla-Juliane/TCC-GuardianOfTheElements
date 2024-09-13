using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float moveSpeed;
    public bool platform1, platform2;
    public bool moveRight = true, moveUp = true;

    // Update is called once per frame
    void Update()
    {
        if (platform1)
        {
            if (transform.position.x > 37.34f)
            {
                moveRight = false;
            }
            else if (transform.position.x < 29.69f)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
            }
        }
        
        if (platform2)
        {
            if (transform.position.y > 3)
            {
                moveUp = false;
            }
            else if (transform.position.y < -1.64f)
            {
                moveUp = true;
            }

            if (moveUp)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.up * -moveSpeed * Time.deltaTime);
            }
        }
        
    }
}
