using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class TestNPC : Interactable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueGraph graph;
    [SerializeField] private bool oneTimeDialogue = true;

    private bool hasDialogeBeenUsed;
    public override string GetDescription()
    {
        return "Talk to the NPC";
    }

    public override void Interact()
    {
        if (oneTimeDialogue && hasDialogeBeenUsed) { return; }
        DialogueUI.Instance.ShowDialogue(graph);
        hasDialogeBeenUsed = true;  
    }

}
