using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthSelection : MonoBehaviour
{
    public LaughBarScript laugh_bar;
    public GameObject frown;
    public GameObject meh;
    public GameObject grin;
    public GameObject laugh;

    public void Update()
    {
        frown.SetActive(laugh_bar.fillAmount < 25);
        meh.SetActive(laugh_bar.fillAmount >= 25 && laugh_bar.fillAmount < 50);
        grin.SetActive(laugh_bar.fillAmount >= 50 && laugh_bar.fillAmount < 75);
        laugh.SetActive(laugh_bar.fillAmount >= 75);
    }
}
