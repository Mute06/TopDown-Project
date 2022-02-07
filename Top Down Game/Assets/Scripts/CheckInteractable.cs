using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInteractable : MonoBehaviour
{
    [SerializeField] private float CheckRadius = 1f;
    [SerializeField] private LayerMask layerMaskToCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit2D castHit = Physics2D.CircleCast(transform.position, CheckRadius, Vector2.zero,0, layerMask:layerMaskToCheck);
        if (castHit.collider != null)
        {
            if (castHit.collider.TryGetComponent<Interactable>(out Interactable interactable))
            {
                
                if (interactable.interactionType == Interactable.InteractionTypes.Talk)
                {
                    
                    if (Input.GetMouseButtonDown(0)) // This will be replaced with a interaction button this is just for test
                    {
                        Debug.Log("Interacted");
                        interactable.Interact();
                        
                    }
                }
            }
        }
    }
}
