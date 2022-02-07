using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPC : Interactable
{
    [SerializeField] private DialogueObject dialogueObject;

    public override string GetDescription()
    {
        return "Talk to the NPC";
    }

    public override void Interact()
    {
        DialogueUI.Instance.ShowDialogue(dialogueObject);
    }
}
