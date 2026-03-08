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
    public void create() 
    {
    }
    
    async void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        await InitUGS();
    }

    private async Task InitUGS()
    {
        // 1️⃣ 제일 먼저 전체 초기화 (필수)
        await UnityServices.InitializeAsync();

        // 2️⃣ 로그인
        await Login();

        /*
        // 3️⃣ 클라우드 세이브 로드
        await Load();*/

        // 4️⃣ 애널리틱스 시작
        AnalyticsService.Instance.StartDataCollection();

        //Debug.Log("UGS Analytics Ready");

        Debug.Log("UGS All Ready");
    }

    private async Task Login()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                AuthenticationService.Instance.SignInAnonymouslyAsync();
                //AuthenticationService.Instance.LinkWithSteamAsync("480");
                // AuthenticationService.Instance.LinkWithGoogleAsync();
                // AuthenticationService.Instance.LinkWithAppleAsync();
            }
            catch (AuthenticationException e)
            {
                Debug.LogError(e);
            }
            catch (RequestFailedException e)
            {
                Debug.LogError(e);
            }
        }

        LocalManager.Instance.playerId = AuthenticationService.Instance.PlayerId;
    }

}
