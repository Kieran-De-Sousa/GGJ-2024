using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudManager : MonoBehaviour
{
    public PlayerHud[] huds;

    public void Awake()
    {
        for(int i = 0; i < 4; i ++)
        {
            huds[i].gameObject.SetActive(GameData.player_count > i);
            huds[i].Init(i);
        }
    }

    private void Update()
    {
        for (int i = 0; i < GameData.player_count; i++)
        {
            huds[i].UpdateHud(ScoreManager.Instance.GetScoreInfo(i).PlayerScore);
        }
    }
}
