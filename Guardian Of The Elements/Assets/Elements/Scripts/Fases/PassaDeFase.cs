using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PassaDeFase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex - 2;
            PlayerPrefs.SetInt("CompletedLevel", sceneIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Map");
        }
    }
}
