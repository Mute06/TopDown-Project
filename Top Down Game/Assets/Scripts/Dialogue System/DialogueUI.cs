using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField] private TMP_Text textLabel, characterName;
    [SerializeField] private Image characterImage;
    [SerializeField] private GameObject Input_Canvas; // To close when on dialogue

    public bool IsOpen { get; private set; }
    private TypeWritterEffect typeWritterEffect;
    private ResponseHandler responseHandler;

    private DialogueGraph activeDialogueGraph;
    private DialogueObject activeDialogueObject;

    private bool characterInfoVisible;
    private float originalCharacterImageWidth, originalCharacterImageHeight;

    private void Start()
    {
        typeWritterEffect = GetComponent<TypeWritterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        originalCharacterImageWidth = characterImage.rectTransform.rect.width;
        originalCharacterImageHeight = characterImage.rectTransform.rect.height;

        CloseDialogueBox();

    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        if (!IsOpen)
        {
            IsOpen = true;
            dialogueBox.SetActive(true);
            Input_Canvas.SetActive(false);
            StartCoroutine(StepThroughDialogue(dialogueObject));
        }
    }

    public void ShowDialogue(DialogueGraph dialogueGraph) // override for temp
    {
        if (!IsOpen)
        {
            IsOpen = true;
            dialogueBox.SetActive(true);
            Input_Canvas.SetActive(false);
            activeDialogueGraph = dialogueGraph;
            dialogueGraph.Restart();
            
            StartCoroutine(StepThroughDialogue(dialogueGraph.current));
        }
    }

    public void PassToNewDialogue(DialogueObject dialogueObject)
    {
        if (IsOpen)
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
        //Draw Chartacter properties
        if (dialogueObject.character != null)
        {
            SetCharacterProperties(dialogueObject.character);
        }

        // Make character name and image invisible if there is no character info
        else
        {
            SetCharacterInfoActive(false);
        }

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
            var _port = dialogueObject.GetOutputPort("output");
            if (_port != null && _port.IsConnected) // if there is no response and there is another dialogueobject connected
            {
                activeDialogueGraph.current = _port.Connection.node as DialogueObject;
                StartCoroutine(StepThroughDialogue(activeDialogueGraph.current));
                yield break;
            }
            else
            {
                CloseDialogueBox();
            }
            
        }

        
    }

    private void SetCharacterImage(Sprite sprite)
    {
        float spriteWidth = sprite.rect.width;
        float spriteHeight = sprite.rect.height;
        float aspectRatio = spriteWidth / spriteHeight;

        if (aspectRatio > 1)
        {
            characterImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalCharacterImageWidth);
            characterImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalCharacterImageWidth / aspectRatio);
        }
        else
        {
            characterImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalCharacterImageHeight);
            characterImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalCharacterImageHeight * aspectRatio);
        }

        characterImage.sprite = sprite;
    }

    private void SetCharacterProperties(CharacterInfo info)
    {
        if (!characterInfoVisible)
        {
            SetCharacterInfoActive(true);
        }
        characterName.text = info.name;
        characterName.color = info.color;
        SetCharacterImage(info.sprite);

    }
    private void SetCharacterInfoActive(bool active)
    {
        if (active)
        {
            characterName.alpha = 1f;
            characterImage.enabled = true;
        }
        else
        {
            characterImage.sprite = null;
            characterName.text = string.Empty;
            characterName.alpha = 0f;
            characterImage.enabled = false;
        }
        characterInfoVisible = active;
    }

    public DialogueBaseNode GetCurrentDialogueObject()
    {
        return activeDialogueGraph.current;
    }

    public void CloseDialogueBox()
    {
        if (IsOpen)
        {
            IsOpen = false;
            dialogueBox.SetActive(false);
            Input_Canvas.SetActive(true);
            textLabel.text = string.Empty;
        }

    }
}
