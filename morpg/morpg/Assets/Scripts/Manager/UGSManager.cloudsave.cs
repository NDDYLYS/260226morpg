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
    public async void Save(int _index)
    {
        //await CloudSaveService.Instance.Data.Player.SaveAsync(
        //    new Dictionary<string, object>
        //    {
        //        { $"PLAYER_SAVE_{_index}", saveData }
        //    }
        //);
    }

    public async Task Load(int _index)
    {
        var keys = new HashSet<string> { $"PLAYER_SAVE_{_index}" };

        var result =
            await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

        if (!result.ContainsKey($"PLAYER_SAVE_{_index}"))
        {
            Debug.Log("저장 데이터 없음");
            return;
        }

        // = result[$"PLAYER_SAVE_{_index}"].Value.GetAs<SaveData>();
    }

    //public async Task<SaveData> GetGold(int slotIndex)
    //{
    //    string key = $"PLAYER_SAVE_{slotIndex}";
    //    var keys = new HashSet<string> { key };

    //    var result = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

    //    if (!result.ContainsKey(key))
    //    {
    //        Debug.Log("not SaveData");
    //        return null;
    //    }

    //    var save = result[key].Value.GetAs<SaveData>();
    //    Debug.Log($"{slotIndex}_Gold : " + save.gold);
    //    return save;
    //}
}
