using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogController : MonoBehaviour
{
    public Text textUI;
    public GameObject dialogDisplay;
    private string[] sentences;
    private int indexSentence;
    public float typingSpeed;
    public float timeBetweenSentences;
    public static UIDialogController Instance;

    void Start()
    {
        dialogDisplay.SetActive(false);
        Instance = this;
    }

    private void Update()
    {
        //skip current sentence.
       if (Input.GetKeyDown(KeyCode.T))
       {
            StopAllCoroutines();
            NextSentence();
       }
    }

    //Sets the array of chars from sentences.
    //and displays letter by letter the sentence in the UI
    IEnumerator Type()
    {
        char[] arraychars = sentences[indexSentence].ToCharArray();
        foreach (char letter in arraychars)
        {
            textUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(timeBetweenSentences);
        NextSentence();
      
    }

    //checks if the sentences is null, and the sentence index.
    //starts next sentence. 
    void NextSentence()
    {
        if(sentences != null)
            if(indexSentence < sentences.Length-1)
            {
                textUI.text = "";
                indexSentence++;        
                StartCoroutine(Type());
            }
            else
            {
                textUI.text = "";
                dialogDisplay.SetActive(false);
            }
    }

    //set the sentences recived.
    public void SetSentences(string[] _sentences )
    {
        sentences = null;
        sentences = new string[_sentences.Length];
        indexSentence = 0;
        for (int i =0; i<_sentences.Length;i++)
        {
            sentences[i] = _sentences[i];           
        }
    }

    //recives the sentences from the Dialog1, and a ctivates the UI and starts the type() corroutine.
    public void DisplayDialog(string[] _sentences) {
        StopAllCoroutines();
        textUI.text = "";
        dialogDisplay.SetActive(true);
        SetSentences(_sentences);
        StartCoroutine(Type());
    }

}
