using UnityEngine;
using UnityEngine.UI;  // Para manipulação da UI

public class InteractionManager : MonoBehaviour
{
    public string[] dialogueLines; // Linhas de diálogo que o NPC dirá
    public GameObject dialogueUI; // Referência à interface de diálogo
    public Text dialogueText; // Texto que exibe as falas
    private int currentLineIndex = 0; // Índice da fala atual
    private bool isPlayerNearby = false; // Verifica se o jogador está próximo

    void Update()
    {
        // Verifica se o jogador pressionou a tecla de interação
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowNextLine();
        }
    }

    void ShowNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            // Finaliza o diálogo
            dialogueUI.SetActive(false);
            currentLineIndex = 0; // Reseta o índice para um futuro diálogo
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            dialogueUI.SetActive(true);
            dialogueText.text = dialogueLines[currentLineIndex];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            dialogueUI.SetActive(false);
            currentLineIndex = 0;
        }
    }
}