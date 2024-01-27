using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
/// <author>Kieran</author>
public abstract class ComedicActionBase : MonoBehaviour
{
    [SerializeField] protected bool comedyTriggered = false;
    [SerializeField] protected float comedyAmount = 0f;
    [SerializeField] protected GameEvent comedyEvent = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ComedyTriggered()
    {
        if (!comedyTriggered)
        {
            comedyTriggered = true;
            comedyEvent.Raise(this, comedyAmount);
        }
    }
}
