using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        using (var request  = new UnityWebRequest(url, "PUT"))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);
            request.SetRequestHeader(BackFourApps.contentType, BackFourApps.appJson);

            var data = new { hasCompletedTutorial = Entity_Player.Instance.UserDatas.hasCompletedTutorial };
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
            Debug.Log(request.downloadHandler.text);
        }

    }
}
