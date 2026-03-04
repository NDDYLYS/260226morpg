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
    private void Analytics_1()
    {
        AnalyticsService.Instance.RecordEvent("attack");
    }

    private void Analytics_2()
    {
        var attackEvent = new CustomEvent("attack")
        {
            { "damage", 120 },
            { "weapon", "Laser" },
            { "critical", true }
        };

        AnalyticsService.Instance.RecordEvent(attackEvent);

    }

    private void Analytics_3()
    {
        AnalyticsService.Instance.RecordEvent(
            new AttackEvent(120, "Laser", true)
        );
    }
}
