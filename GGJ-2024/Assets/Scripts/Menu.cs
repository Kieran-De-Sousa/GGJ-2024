using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Animator anim;

    public void Play()
    {
        anim.SetTrigger("JoinStart");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ExitJoin()
    {
        anim.SetTrigger("JoinEnd");
    }
}
