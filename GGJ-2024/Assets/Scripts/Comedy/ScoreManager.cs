using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ScoreInfo // class because i cba to think about c#'s nullable stuff rn. just dont edit it :)
{
    public int PlayerId;
    public float PlayerScore;
}

/// <summary>
/// Tracks all the players scores.
/// </summary>
public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] bool _logScores = true;
    List<ScoreInfo> _playerScores = new();

    void Start()
    {
#if UNITY_EDITOR
        const float repeatRate = 10.0f;
        InvokeRepeating(nameof(LogScoresToConsole), 0.0f, repeatRate);
#endif // ifdef UNITY_EDITOR
    }

    public void OnComedyEvent(Component sender, object data)
    {
        if (sender is TestPlayerScript && data is float)
        {
            AddScore((sender as TestPlayerScript).PlayerIndex, (float)data);
        }
        else
        {
            Debug.Assert(false, "Received a comedy event, but the data is in the wrong format!");
        }
    }

    void AddScore(int playerId, float score)
    {
        ScoreInfo playerScoreInfo = GetScoreInfo(playerId);
        if (playerScoreInfo == null)
        {
            playerScoreInfo = new ScoreInfo
            {
                PlayerId = playerId,
                PlayerScore = 0
            };
            _playerScores.Add(playerScoreInfo);
        }

        playerScoreInfo.PlayerScore += score;
        Debug.Log($"Adding {score} score for player id {playerId}. Total score is now {playerScoreInfo.PlayerScore}.");
    }

    public ScoreInfo GetHighestScore() => _playerScores.OrderBy(p => p.PlayerScore).LastOrDefault();
    public ScoreInfo GetScoreInfo(int playerId)
    {
        ScoreInfo info = _playerScores.Where(p => p.PlayerId == playerId).FirstOrDefault();
        if (info == null)
        {
            info = new ScoreInfo
            {
                PlayerId = playerId,
                PlayerScore = 0
            };
            _playerScores.Add(info);
        }
        return info;
    }
    public ScoreInfo GetScoreInfo(TestPlayerScript playerScript) => GetScoreInfo(playerScript.PlayerIndex);
    public void ClearAllScores() => _playerScores = new();

    void LogScoresToConsole()
    {
        if (!_logScores) return;
        if (_playerScores == null || _playerScores.Count == 0) return;

        StringBuilder stringBuilder = new();
        stringBuilder.Append($"Total players: {_playerScores.Count} | ");
        foreach (var playerScore in _playerScores)
        {
            stringBuilder.Append($"Player: {playerScore.PlayerId} - Score: {playerScore.PlayerScore} ");
        }
        Debug.Log(stringBuilder.ToString());
    }
}
