using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractionTypes
    {
        Talk,
        Pickup,
    }

    public InteractionTypes interactionType;
    public abstract string GetDescription();
    public abstract void Interact();
}
