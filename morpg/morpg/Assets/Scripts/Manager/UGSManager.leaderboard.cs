using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Threading.Tasks;

public partial class UGSManager : SingletonGameObject<UGSManager>
{
    public async void Leaderboard(int _score)
    {
        var isLogin = AuthenticationService.Instance.IsSignedIn;
        if (!isLogin)
            return;

        await LeaderboardsService.Instance.AddPlayerScoreAsync("test", _score);
    }

    public async void ShowLeaderboard()
    {
        var isLogin = AuthenticationService.Instance.IsSignedIn;
        if (!isLogin)
            return;

        var scores = await LeaderboardsService.Instance.GetScoresAsync("test");

        foreach (var entry in scores.Results)
        {
            Debug.Log($"Rank:{entry.Rank} Score:{entry.Score} Player:{entry.PlayerId}");
        }
    }

}
