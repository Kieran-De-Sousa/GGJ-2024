using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractorBase : MonoBehaviour
{

    [SerializeField] protected List<Interactable> interactables = new();


    public virtual void Interact()
    {
        foreach(var interactable in interactables)
        {
            interactable.Interact();
        }
    }
}