using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Image black;
    //public Animator anim;

    public void PlayScene(string sceneName)
    {
        Debug.Log("Acessar cena");
        //StartCoroutine(Fading(sceneName));
        SceneManager.LoadScene(sceneName);
    }

    /*IEnumerator Fading(string sceneName)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a==1);
        SceneManager.LoadScene(sceneName);
    }*/
}
