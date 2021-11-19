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
    // [HideInInspector] = variavel publlica q nn aarece no inspector
    private bool isShowing; // se a janela esta visivel
    private int index; // index das sentencas
    private string[] sentences; // lista das sentencas
    Sprite[] sprites;
    string[] actorName; //criei esse array para coletar o nome em cada fala

    public static DialogueControl instance;

    public bool IsShowing { get => isShowing; set => isShowing = value; }

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
                profileSprite.sprite = sprites[index];
                actorNameText.text = actorName[index]; //aqui o text recebe o nome de quem fala do array
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
    public void Speech(string[] txt, Sprite[] spr, string[] nameTxt)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            sprites = spr;
            actorName = nameTxt; //aqui coloco os nomes no array actorName
            StartCoroutine(TypeSentence());
            profileSprite.sprite = sprites[index]; //mostra o primeiro sprite
            actorNameText.text = actorName[index]; //mostra o primeiro nome
            isShowing = true;
        }
    }
}
