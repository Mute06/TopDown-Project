using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[Serializable]


public class DialogueObject : DialogueBaseNode
{
    public CharacterInfo character;
    [TextArea(3,100)] public string[] dialogue;

    
    [Output(dynamicPortList = true)] public string[] responses;


    public string[] Dialogue
    {
        get { return dialogue; }
    }
    public string[] Responses
    {
        get { return responses; }
    }

    public bool HasResponses
    {
        get { return Responses != null && Responses.Length > 0; }
    }

    public override object GetValue(NodePort port)
    {
        return null;
    }

    public override void Trigger()
    {
        (graph as DialogueGraph).current = this;
    }
}
