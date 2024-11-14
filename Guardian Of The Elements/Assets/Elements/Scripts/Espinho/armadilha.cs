using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadilha : MonoBehaviour
{
    public Vector2 intervalo;
    private Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim. enabled = false;
    }
        
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(intervalo.x, intervalo.y));
        anim.enabled = true;
    }
     
 
    
}
