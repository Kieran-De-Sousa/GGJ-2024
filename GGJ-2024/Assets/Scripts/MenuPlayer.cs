using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    public void AttemptStart()
    {
        FindObjectOfType<Menu>().AttemptStart();
    }
}
