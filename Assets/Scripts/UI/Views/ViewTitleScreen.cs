using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Player;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ViewTitleScreen : ViewElementButton
{
    [SerializeField] public Button quitButton { get; private set; }

    protected override void OnStart()
    {
        base.OnStart();
        OnWebGLCleanup();
    }

    public void OnWebGLCleanup()
    {
#if UNITY_WEBGL
        if (quitButton)
        {
            Destroy(quitButton.gameObject);
        }
#endif
    }

    public void Logout()
    {
        UIManager uiManager = UIManager.Instance;
        uiManager.ViewPromoCode.OnHide();
        uiManager.ViewController.SwitchViewSequential(uiManager.ViewLogin);
        UpdateUserDatas();
        //Entity_Player.Instance.UserDatas = null;
    }

    public void UpdateUserDatas() { StopAllCoroutines(); StartCoroutine(UpdateUserDatasIE()); }
    public IEnumerator UpdateUserDatasIE() // Get User from data base
    {
        string url = $"{BackFourApps.urlClasses}UserData/{Entity_Player.Instance.UserDatas.userDataId}";
        using (var request = new UnityWebRequest(url, "PUT"))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);
            request.SetRequestHeader(BackFourApps.contentType, BackFourApps.appJson);

            UpdatePlayerStatsOnLogOut();
            
            var data = new
            {
                hasCompletedTutorial = Entity_Player.Instance.UserDatas.userDatasGameplay.hasCompletedTutorial,
                PersistantStats = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "BasePlayerStats.tsv")))
            };
            var json = JsonConvert.SerializeObject(data);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogWarning("ERROR: " + request.error);
                Debug.LogWarning("TEST: " + request.result);
#endif
                yield break;
            }
        }
        
    }
    private void UpdatePlayerStatsOnLogOut()
    {
        PlayerStatsSO _playerStats = Entity_Player.Instance.baseStats;
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "BasePlayerStats.tsv"), "");

        StreamWriter writer = new StreamWriter(Path.Combine(Application.streamingAssetsPath, "BasePlayerStats.tsv"));
        writer.WriteLine("MaxHealth\t" + _playerStats.MaxHealth);
        writer.WriteLine("MoveSpeed\t" + _playerStats.MoveSpeed);
        writer.WriteLine("AttackSpeed\t" + _playerStats.AttackSpeed);
        writer.WriteLine("BoomDistance\t" + _playerStats.BoomDistance);
        writer.WriteLine("BoomAttackSpeed\t" + _playerStats.BoomAttackSpeed);
        writer.WriteLine("DodgeDelay\t" + _playerStats.DodgeDelay);
        writer.WriteLine("BigGold\t" + _playerStats.BigGold);
        writer.WriteLine("SmallGold\t" + _playerStats.SmallGold);
        writer.Close();

    }
    
}
