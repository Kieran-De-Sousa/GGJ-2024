using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Interact() {}
    public virtual void Interact(TestPlayerScript playerScript) {}
}