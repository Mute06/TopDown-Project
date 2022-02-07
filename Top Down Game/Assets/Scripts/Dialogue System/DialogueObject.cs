using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialogue
    {
        get { return dialogue; }
    }
    public Response[] Responses
    {
        get { return responses; }
    }

    public bool HasResponses
    {
        get { return Responses != null && Responses.Length > 0; }
    }
}
