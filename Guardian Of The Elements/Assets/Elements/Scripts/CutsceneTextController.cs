using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneTextController : MonoBehaviour
{
    public TextMeshProUGUI texto; // Referência ao componente TextMeshProUGUI
    public float tempoPorLetra = 0.05f; // Tempo entre cada letra
    public string[] frases; // Array de frases que aparecerão na cutscene

    public Image fundoCutscene; // Referência à imagem de fundo no Canvas
    public Sprite[] fundos; // Array de sprites para os fundos

    private int indexFrase = 0; // Índice da frase atual
    public string nomeCenaProxima = "NomeDaProximaCena"; // Nome da cena a carregar

    private void Start()
    {
        // Define o fundo inicial, se houver
        if (fundos.Length > 0)
        {
            fundoCutscene.sprite = fundos[0];
        }

        // Começa a exibir a primeira frase
        StartCoroutine(ExibirTexto(frases[indexFrase]));
    }

    IEnumerator ExibirTexto(string frase)
    {
        texto.text = ""; // Limpar texto antes de começar a exibir a frase

        foreach (char letra in frase)
        {
            texto.text += letra; // Adiciona a próxima letra à frase exibida
            yield return new WaitForSeconds(tempoPorLetra); // Aguarda o tempo antes de mostrar a próxima letra
        }

        yield return new WaitForSeconds(0.2f); // Tempo reduzido entre frases

        // Troca o fundo, se houver mais fundos disponíveis
        if (indexFrase < fundos.Length - 1)
        {
            fundoCutscene.sprite = fundos[indexFrase + 1];
        }

        // Avança para a próxima frase, se houver
        indexFrase++;
        if (indexFrase < frases.Length)
        {
            StartCoroutine(ExibirTexto(frases[indexFrase]));
        }
        else
        {
            Debug.Log("Finalizando cutscene e trocando de cena...");
            TrocarCena(); // Chama imediatamente ao final da última frase
        }
    }

    private void TrocarCena()
    {
        StartCoroutine(CarregarCenaAssincronamente());
    }

    IEnumerator CarregarCenaAssincronamente()
    {
        AsyncOperation operacao = SceneManager.LoadSceneAsync(nomeCenaProxima);

        // Opcional: esperar o carregamento
        while (!operacao.isDone)
        {
            yield return null; // Aguarda o próximo frame
        }
    }
}
