using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    #region Singleton
    private static DialogueUI instance;

    public static DialogueUI Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            //if there is already a instance then destroy this one
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion



    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject Input_Canvas; // To close when on dialogue

    public bool isOpen { get; private set; }
    private TypeWritterEffect typeWritterEffect;
    private ResponseHandler responseHandler;

    private void Start()
    {
        typeWritterEffect = GetComponent<TypeWritterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();

    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        if (!isOpen)
        {
            isOpen = true;
            dialogueBox.SetActive(true);
            Input_Canvas.SetActive(false);
            StartCoroutine(StepThroughDialogue(dialogueObject));
        }
    }
    public void PassToNewDialogue(DialogueObject dialogueObject)
    {
        if (isOpen)
        {
            StartCoroutine(StepThroughDialogue(dialogueObject));
        }
        else
        {
            ShowDialogue(dialogueObject);
        }
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typeWritterEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses)
            {
                break;
            } 

            yield return new WaitUntil( () => Input.GetMouseButtonDown(0));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }

        
    }

    private void CloseDialogueBox()
    {
        if (isOpen)
        {
            isOpen = false;
            dialogueBox.SetActive(false);
            Input_Canvas.SetActive(true);
            textLabel.text = string.Empty;
        }

    }
}
