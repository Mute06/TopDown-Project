using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ResponseHandler : MonoBehaviour
{
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

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0f;

        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(respongeButtonPrefab, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener( () => OnPickedResponse(response) );
            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        responseBox.gameObject.SetActive(false);

        foreach (var button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();

        dialogueUI.PassToNewDialogue(response.Dialogue);
    }
}
