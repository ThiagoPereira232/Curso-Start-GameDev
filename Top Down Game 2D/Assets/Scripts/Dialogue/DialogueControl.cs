using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; // janela do dialogo
    public Image profileSprite; // spreite do pergil
    public Text speechText; // texto da fala
    public Text actorNameText; // nome do npc

    [Header("Settings")]
    public float typingSpeed; // velocidade da fala

    // Variaveis de controle
    private bool isShowing; // se a janela esta visivel
    private int index; // index das sentencas
    private string[] sentences; // lista das sentencas

    public static DialogueControl instance;

    // awake é chamado antes de todos os start() na hierarquia de execução dos scripts
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // co-rotina, controla por tempo
    IEnumerator TypeSentence()
    {
        // char armazena um, letra por letra
        foreach (char letter in sentences[index].ToCharArray()) 
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // pular para proxima frase/fala
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length-1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());

            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
        }
    }

    // chamar a fala do npc
    public void Speech(string[] txt)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
