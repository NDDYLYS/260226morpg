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
using JetBrains.Annotations;

public partial class UGSManager : SingletonGameObject<UGSManager>
{
    private Dictionary<int, SaveData> saveDataDics;
    [SerializeField] private bool returnBool;

    public SaveData GetSaveData(int _index)
    {
        if (saveDataDics.ContainsKey(_index))
        {
            return saveDataDics[_index];
        }

        return null;
    }

    public async void Save(int _index)
    {
        if (_index <= 0)
            return;
        if (Constant.dataSlot < _index)
            return;

        var saveData = GameManager.Instance.SaveData;

        await CloudSaveService.Instance.Data.Player.SaveAsync(
            new Dictionary<string, object>
            {
                { $"PLAYER_SAVE_{_index}", saveData }
            }
        );

        refreshSaveData();
    }

    public async void Load(int _index)
    {
        if (_index <= 0)
            return;

        var keys = new HashSet<string> { $"PLAYER_SAVE_{_index}" };

        var result =
            await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

        if (!result.ContainsKey($"PLAYER_SAVE_{_index}"))
        {
            //Debug.Log("저장 데이터 없음");
            return;
        }

        var saveData = result[$"PLAYER_SAVE_{_index}"].Value.GetAs<SaveData>();

        GameManager.Instance.SetGameState(GameStateEnum.Play);
        GameManager.Instance.SaveData = saveData;
        GameManager.Instance.MovingScene("2Village");
        TableDataManager.Instance.updateSavedata(saveData);
    }

    public async Task refreshSaveData()
    {
        if (saveDataDics == null)
            saveDataDics = new Dictionary<int, SaveData>();
        else
            saveDataDics.Clear();

        returnBool = false;

        var slot = Constant.dataSlot;
        for (var i = 0; i < slot; i++) 
        {
            var keys = new HashSet<string> { $"PLAYER_SAVE_{i}" };
            var result = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

            if (!result.ContainsKey($"PLAYER_SAVE_{i}"))
            {
                saveDataDics.Add(i, null);
            }
            else 
            {
                var saveData = result[$"PLAYER_SAVE_{i}"].Value.GetAs<SaveData>();
                saveDataDics.Add(i, saveData);

                returnBool = true;
            }
        }
    }

    public bool getReturnBool() 
    {
        return returnBool;
    }
}
