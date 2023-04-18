using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UserLoginController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private PlayerStatsSO _playerStats;

    public static string sessionToken;

    [Header("Logins")] [SerializeField] private TMP_InputField inputFieldEmail;
    [SerializeField] private TMP_InputField inputFieldPassword;

    [Header("Visual cue")] [SerializeField]
    private float cueVisibleTime = 3;

    [SerializeField] private TMPSceneBasedLocalizable localizableCueEmailVerification;
    [Space] [SerializeField] private TMPSceneBasedLocalizable localizableCueValid;
    [SerializeField] private string keyValidLogin;
    [SerializeField] private string keyValidSignup;
    [Space] [SerializeField] private TMPSceneBasedLocalizable localizableCueInvalid;
    [SerializeField] private string keyInvalidEmailCombination;
    private TMPSceneBasedLocalizable _activeCue;
    private SequentialTimer _timerDelayedGotoTitleScreen;
    private ErrorHandler _errorHander;

    private void Awake()
    {
        _timerDelayedGotoTitleScreen = new(cueVisibleTime, () => { GotoTitleScreen(); });
        _timerDelayedGotoTitleScreen.Reset(true);
        _errorHander = GetComponent<ErrorHandler>();
    }

    public void PlayInOfflineMode()
    {
        SetInteractability(false);
        SwitchActiveCue(localizableCueValid);
        _activeCue.LocalizeExternalText(keyValidSignup);

        Entity_Player.Instance.UserDatas = new();
        Entity_Player.Instance.UserDatas.userDatasGameplay = new();
        GotoTitleScreen();
    }

    private void GotoTitleScreen()
    {
        SwitchActiveCue(null);
        _timerDelayedGotoTitleScreen.Reset(isPaused: true);
        SceneLoadManager.Instance.GoToTitleScreen();
        localizableCueEmailVerification.gameObject.SetActive(false);
    }

    private void Update()
    {
        _timerDelayedGotoTitleScreen.OnUpdateTime();
    }

    public void SetInteractability(bool interactability)
    {
        canvasGroup.interactable = interactability;
        canvasGroup.blocksRaycasts = interactability;
    }

    public void SignUp()
    {
        StopAllCoroutines();
        StartCoroutine(SignUpIE());
    }

    public IEnumerator SignUpIE()
    {
        // TODO CHECK IF EMAIL EXISTS HERE
/*        //string url = $"{BackFourApps.urlUsers}?email={inputFieldEmail.text}";
        string url = "https://parseapi.back4app.com/classes/users?where={\"email\":\"" + inputFieldEmail.text + "\"}";
        using (var request = new UnityWebRequest(url, "GET"))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);

            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogWarning("ERROR: " + request.error);
#endif
                SwitchActiveCue(localizableCueInvalid);
                // TODO: Make COR checks here
                //string key = _errorHander.TryLoginHandleError();
                //_activeCue.LocalizeExternalText(key); // TODO: Pass check key value here
                _activeCue.LocalizeExternalText("error email already in use"); // TODO: Remove when above is implemented

                Debug.Log(request.downloadHandler.text);
                Debug.Log(request.downloadHandler.text.Contains(inputFieldEmail.text));

                yield break;
            }

            Debug.Log("NO EMAIL FOUND");
        }*/


        // UserData Row Creation
        // Required for giving a foreign key to the user to access his or her datas
        string tempUserDataId = "";
        using (var request = new UnityWebRequest(BackFourApps.urlUserData, "POST"))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);
            request.SetRequestHeader(BackFourApps.contentType, BackFourApps.appJson);

            var data = new { };
            var json = JsonConvert.SerializeObject(data);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogWarning("ERROR: couldn't create a row at UserData class");
#endif
                yield break;
            }

            tempUserDataId = GetUserDatas(request).objectId;
        }

        // User Row Creation
        using (var request = new UnityWebRequest(BackFourApps.urlUsers, "POST"))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);
            request.SetRequestHeader(BackFourApps.revocSession, "1");
            request.SetRequestHeader(BackFourApps.contentType, BackFourApps.appJson);

            var data = new
            {
                username = inputFieldEmail.text, email = inputFieldEmail.text, password = inputFieldPassword.text,
                userDataId = tempUserDataId
            };
            var json = JsonConvert.SerializeObject(data);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            // TODO: Make COR checks here for email format validity

            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogWarning("ERROR: " + request.error);
#endif
                SwitchActiveCue(localizableCueInvalid);


                // TODO: Make COR checks here for email already exists
                //string key = _errorHander.TryLoginHandleError();
                //_activeCue.LocalizeExternalText(key); // TODO: Pass check key value here
                //_activeCue.LocalizeExternalText("TEST"); // TODO: Remove when above is implemented
            }

            if (_errorHander.TrySignUpHandleError(inputFieldEmail.text, inputFieldPassword.text))
            {
                _activeCue.LocalizeExternalText(_errorHander.errorKey);
                yield break;
            }

            StartCoroutine(PostPlayerStatsFile());

            SwitchActiveCue(localizableCueValid);
            _activeCue.LocalizeExternalText(keyValidSignup);
        }
    }

    public void LogIn()
    {
        StopAllCoroutines();
        StartCoroutine(LogInIE());
    }

    public IEnumerator LogInIE() // Get User from data base
    {
        string url = $"{BackFourApps.urlLogin}?username={inputFieldEmail.text}&password={inputFieldPassword.text}";
        using (var request = new UnityWebRequest(url, "GET"))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);
            request.SetRequestHeader(BackFourApps.revocSession, "1");

            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogWarning("ERROR: " + request.error);
#endif
                SwitchActiveCue(localizableCueInvalid);

                if (_errorHander.TryLoginHandleError(inputFieldEmail.text, inputFieldPassword.text))
                {
                    _activeCue.LocalizeExternalText(_errorHander.errorKey);
                }
                else
                {
                    _activeCue.LocalizeExternalText("error wrong combination");
                }

                yield break;
                // TODO: Make COR checks here
                //string key = _errorHander.TryLoginHandleError();
                //_activeCue.LocalizeExternalText(key); // TODO: Pass check key value here
                //_activeCue.LocalizeExternalText("error wrong combination"); // TODO: Remove when above is implemented
            }

            SetUserDatas(request);

            if (!Entity_Player.Instance.UserDatas.emailVerified)
            {
                SwitchActiveCue(localizableCueEmailVerification);
                yield break;
            }

            var jObject = JObject.Parse(request.downloadHandler.text);
            sessionToken = jObject["sessionToken"].ToString();

            SwitchActiveCue(localizableCueValid);
            _activeCue.LocalizeExternalText(keyValidLogin);

            GoToTitleScreenHandler();
        }

        // Set user datas
        url = $"{BackFourApps.urlUserData}/{Entity_Player.Instance.UserDatas.userDataId}";
        using (var request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("ERROR: NO USERDATA ON GET+ " + request.error);
                yield break;
            }

            // TODO: Debug bellow, it should work as GetUserDatas() also work (Read online that NewtonJson hates boolean https://github.com/dotnet/runtime/issues/29960)

            //Debug.Log(request.downloadHandler.text);
            //Debug.Log("Is it true: " + GetUserDatasGameplay(request).hasCompletedTutorial);
            //SetUserDatasGameplay(request);

            Entity_Player.Instance.UserDatas.userDatasGameplay = new();
            var matches = Regex.Matches(request.downloadHandler.text, "\"hasCompletedTutorial\":(\\w+)",
                RegexOptions.Multiline);
            Entity_Player.Instance.UserDatas.userDatasGameplay.hasCompletedTutorial =
                matches.First().Groups[1].Value == "true";


            GetPlayerStatsOnLogin();
            Entity_Player.Instance.InitPlayer();
            Entity_Player.Instance.RefreshPlayerStats();
        }
    }

    public void UpdateUserDatas()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateUserDatasIE());
    }

    public IEnumerator UpdateUserDatasIE() // Get User from data base
    {
        string url = $"{BackFourApps.urlClasses}UserData/{Entity_Player.Instance.UserDatas.userDataId}";
        using (var request = new UnityWebRequest(url, "PUT"))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);
            request.SetRequestHeader(BackFourApps.contentType, BackFourApps.appJson);

            UpdatePlayerStatsOnLogOut();
            StartCoroutine(PostPlayerStatsFile());

            var data = new
            {
                hasCompletedTutorial = Entity_Player.Instance.UserDatas.userDatasGameplay.hasCompletedTutorial,
                PersistantStats =
                    Convert.ToBase64String(File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath,
                        "BasePlayerStats.tsv")))
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

    /// <summary>The logic uses Newtonsoft Json package</summary>
    private void SetUserDatas(UnityWebRequest request)
    {
        UserDatas userDatas = GetUserDatas(request);
        Entity_Player.Instance.UserDatas = userDatas;
    }

    private UserDatas GetUserDatas(UnityWebRequest request) =>
        JsonConvert.DeserializeObject<UserDatas>(request.downloadHandler.text);

    /// <summary>The logic uses Newtonsoft Json package</summary>
    private void SetUserDatasGameplay(UnityWebRequest request)
    {
        Debug.Log(request.downloadHandler.text);
        UserDatasGameplay userDatas = GetUserDatasGameplay(request);
        Entity_Player.Instance.UserDatas.userDatasGameplay = userDatas;
    }

    private UserDatasGameplay GetUserDatasGameplay(UnityWebRequest request) =>
        JsonConvert.DeserializeObject<UserDatasGameplay>(request.downloadHandler.text);

    private void GoToTitleScreenHandler()
    {
        SetInteractability(false);
        _timerDelayedGotoTitleScreen.StartTimer();
    }

    private void SwitchActiveCue(TMPSceneBasedLocalizable cue)
    {
        if (_activeCue == cue)
        {
            return;
        }

        if (_activeCue != null)
        {
            _activeCue.gameObject.SetActive(false);
        }

        _activeCue = cue;
        if (_activeCue != null)
        {
            _activeCue.gameObject.SetActive(true);
        }
    }

    private void GetPlayerStatsOnLogin()
    {
        _playerStats = Entity_Player.Instance.baseStats;

        StartCoroutine(GetPlayerStatsFile(SetPlayerSO));
    }

    private void SetPlayerSO()
    {
        List<float> list = new();
        list.Clear();
        string text = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "BasePlayerStats.tsv"));
        string[] lines = text.Split('\n');

        for (int line = 0; line < lines.Length - 1; line++)
        {
            list.Add(float.Parse(lines[line].Split("\t")[1]));
        }

        _playerStats.MaxHealth = (int)list[0];
        _playerStats.MoveSpeed = list[1];
        _playerStats.AttackSpeed = list[2];
        _playerStats.BoomDistance = list[3];
        _playerStats.BoomAttackSpeed = list[4];
        _playerStats.DodgeDelay = list[5];
        _playerStats.BigGold = (int)list[6];
        _playerStats.SmallGold = (int)list[7];
    }

    private IEnumerator GetPlayerStatsFile(Action SetSO)
    {
        string url = $"{BackFourApps.urlUserData}{Entity_Player.Instance.UserDatas.userDataId}";
        using (var request = new UnityWebRequest(url, "GET"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", BackFourApps.ZombieSurvivor.restApiKey);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.downloadHandler.text);
                yield break;
            }

            Debug.Log(request.downloadHandler.text);

            var jObject = JObject.Parse(request.downloadHandler.text);
            var persistantStats = jObject["PersistantStats"].ToString();
            Debug.Log(persistantStats);

            using (var requests = UnityWebRequest.Get(persistantStats))
            {
                yield return requests.SendWebRequest();
                if (requests.result != UnityWebRequest.Result.Success)

                {
                    Debug.LogError(requests.error);
                    Debug.Log(requests.downloadHandler.text);
                    yield break;
                }

                File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "BasePlayerStats.tsv"),
                    requests.downloadHandler.data);
                SetSO.Invoke();
            }
        }
    }

    private IEnumerator PostPlayerStatsFile()
    {
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/files/PlayerStats.tsv", "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", BackFourApps.ZombieSurvivor.restApiKey);
            request.SetRequestHeader("Content-Type", "text/tab-separated-values");

            string filePath = Path.Combine(Application.streamingAssetsPath, "BasePlayerStats.tsv");

            request.uploadHandler = new UploadHandlerFile(filePath);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.downloadHandler.text);
                yield break;
            }
            
            var jObject = JObject.Parse(request.downloadHandler.text);
            var fileurl = jObject["url"].ToString();

            string json = JsonConvert.SerializeObject(new { PersistantStats = fileurl });

            using (var requests =
                   UnityWebRequest.Put($"{BackFourApps.urlUserData}/{Entity_Player.Instance.UserDatas.userDataId}",
                       json))
            {
                requests.SetRequestHeader("X-Parse-Application-Id", BackFourApps.ZombieSurvivor.applicationId);
                requests.SetRequestHeader("X-Parse-REST-API-Key", BackFourApps.ZombieSurvivor.restApiKey);
                requests.SetRequestHeader("X-Parse-Session-Token", sessionToken);
                requests.SetRequestHeader("Content-Type", "application/json");

                yield return requests.SendWebRequest();

                if (requests.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.downloadHandler.text);
                }

            }
        }
    }

    private void UpdatePlayerStatsOnLogOut()
    {
        _playerStats = Entity_Player.Instance.baseStats;
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