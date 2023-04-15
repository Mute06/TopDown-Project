using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private Color[] responseColors;
    [SerializeField] private RectTransform responseBox, responseContainer;
    [SerializeField] private GameObject respongeButtonPrefab;
    private RectTransform responseButtonTemplate;

    private DialogueUI dialogueUI;

    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        responseButtonTemplate = respongeButtonPrefab.GetComponent<RectTransform>();
    }

    public void ShowResponses(string[] responses)
    {
        float responseBoxHeight = 0f;
        int responseIndex = 0;
        foreach (string response in responses)
        {
            GameObject responseButton = Instantiate(respongeButtonPrefab, responseContainer);
            responseButton.SetActive(true);
            Image optionBG = responseButton.GetComponentInChildren<Image>();
            responseButton.GetComponentInChildren<TMP_Text>().text = response;
            int index = responseIndex;
            responseButton.GetComponent<Button>().onClick.AddListener( () => OnPickedResponse(index) );
            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;

            for (int i = 0; i < responseColors.Length; i++)
            {
                if (responseIndex % responseColors.Length == i)
                {
                    optionBG.color = responseColors[i];
                    break;
                }
            }


            responseIndex++;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(int ResponseIndex)
    {
        responseBox.gameObject.SetActive(false);

        foreach (var button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();
        Debug.Log(ResponseIndex);
        var _port = dialogueUI.GetCurrentDialogueObject().GetPort("responses " + ResponseIndex);
        if (_port != null && _port.IsConnected)
        {
            
            if (_port.Connection.node.GetType() == typeof(DialogueObject))
            {
                dialogueUI.PassToNewDialogue(_port.Connection.node as DialogueObject);
            }
            (_port.Connection.node as DialogueBaseNode).Trigger();
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }
    }
}
