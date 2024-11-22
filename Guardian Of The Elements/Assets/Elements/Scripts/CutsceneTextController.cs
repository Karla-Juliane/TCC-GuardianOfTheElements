using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneTextController : MonoBehaviour
{
    public TextMeshProUGUI texto; // Referência ao componente TextMeshProUGUI
    public float tempoPorLetra = 0.05f; // Tempo entre cada letra
    public string[] frases; // Array de frases que aparecerão na cutscene

    public Image fundoCutscene; // Referência à imagem de fundo no Canvas
    public Sprite[] fundos; // Array de sprites para os fundos

    private int indexFrase = 0; // Índice da frase atual

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

        yield return new WaitForSeconds(1f); // Pausa antes de avançar para a próxima frase

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
            Debug.Log("Fim da cutscene de texto.");
        }
    }
}

