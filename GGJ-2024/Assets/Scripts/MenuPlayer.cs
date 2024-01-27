using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    /// <summary>
    /// Attempt to start game from player join screen
    /// </summary>
    public void AttemptStart()
    {
        FindObjectOfType<Menu>()?.AttemptStart();
    }
}
