using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Para carregar cenas

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText; // Referência ao TextMeshPro para o texto do diálogo
    public GameObject dialogBox; // Referência ao painel da caixa de diálogo
    public Button nextButton; // Referência ao botão para avançar no diálogo
    public float letterSpeed = 0.05f; // Tempo entre as letras

    private string[] dialogueLines; // Linhas de diálogo
    private int currentLine = 0; // Linha atual do diálogo

    private void Start()
    {
        // Verifique se os componentes estão configurados corretamente
        if (dialogText == null || dialogBox == null || nextButton == null)
        {
            Debug.LogError("Faltando referências nos componentes! Verifique o Inspector.");
            return;
        }

        // Defina as linhas de diálogo
        dialogueLines = new string[]
        {
            "Olá, bem-vindo ao mundo de Naturia!",
            "Eu sou o Mestre Merlin, e você deve nos ajudar.",
            "Aperte C para atacar, espaço para pular e setas para se mover!"
        };

        // Inicialmente, o painel de diálogo estará invisível
        dialogBox.SetActive(false);

        // Configurar o botão para avançar as linhas
        nextButton.onClick.AddListener(NextLine);

        // Iniciar o diálogo
        StartDialogue();
    }

    // Iniciar o diálogo
    public void StartDialogue()
    {
        dialogBox.SetActive(true); // Exibe a caixa de diálogo
        ShowLine(dialogueLines[currentLine]); // Exibe a primeira linha
    }

    // Mostrar uma linha de diálogo uma letra por vez
    private void ShowLine(string line)
    {
        dialogText.text = ""; // Limpa o texto anterior
        StopAllCoroutines(); // Para qualquer corrotina anterior
        StartCoroutine(TypeSentence(line)); // Inicia a corrotina para digitar a sentença
    }

    // Corrotina para digitar a sentença
    private IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence)
        {
            dialogText.text += letter; // Adiciona uma letra ao texto
            yield return new WaitForSeconds(letterSpeed); // Aguarda um tempo antes de mostrar a próxima letra
        }
    }

    // Avançar para a próxima linha de diálogo
    private void NextLine()
    {
        currentLine++; // Avança para a próxima linha de diálogo
        if (currentLine < dialogueLines.Length)
        {
            ShowLine(dialogueLines[currentLine]);
        }
        else
        {
            EndDialogue(); // Finaliza o diálogo
        }
    }

    // Finaliza o diálogo e pode carregar outra cena
    private void EndDialogue()
    {
        // Aqui, podemos carregar outra cena após o diálogo
        SceneManager.LoadScene("1Terra"); // Troque pelo nome da cena que você deseja carregar
    }
}


