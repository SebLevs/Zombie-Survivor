using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UserLoginController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Logins")]
    [SerializeField] private TMP_InputField inputFieldEmail;
    [SerializeField] private TMP_InputField inputFieldPassword;

    [Header("Visual cue")]
    [SerializeField] private float cueVisibleTime = 3;
    [SerializeField] private TMPSceneBasedLocalizable localizableCueEmailVerification;
    [Space]
    [SerializeField] private TMPSceneBasedLocalizable localizableCueValid;
    [SerializeField] private string keyValidLogin;
    [SerializeField] private string keyValidSignup;
    [Space]
    [SerializeField] private TMPSceneBasedLocalizable localizableCueInvalid;
    [SerializeField] private string keyInvalidEmailCombination;
    private TMPSceneBasedLocalizable _activeCue;
    private SequentialTimer _timerDelayedGotoTitleScreen;
    private ErrorHandler _errorHander;

    private void Awake()
    {
        _timerDelayedGotoTitleScreen = new(cueVisibleTime, () =>
        {
            GotoTitleScreen();
        });
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

    public void SignUp() { StopAllCoroutines(); StartCoroutine(SignUpIE()); }
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

            var data = new {};
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

            var data = new { username = inputFieldEmail.text, email = inputFieldEmail.text, password = inputFieldPassword.text, userDataId = tempUserDataId };
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
            

            SwitchActiveCue(localizableCueValid);
            _activeCue.LocalizeExternalText(keyValidSignup);
        }
    }

    public void LogIn() { StopAllCoroutines(); StartCoroutine(LogInIE()); }
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
            var matches = Regex.Matches(request.downloadHandler.text, "\"hasCompletedTutorial\":(\\w+)", RegexOptions.Multiline);
            Entity_Player.Instance.UserDatas.userDatasGameplay.hasCompletedTutorial = matches.First().Groups[1].Value == "true";
        }
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

            var data = new { hasCompletedTutorial = Entity_Player.Instance.UserDatas.userDatasGameplay.hasCompletedTutorial };
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

    private UserDatas GetUserDatas(UnityWebRequest request) => JsonConvert.DeserializeObject<UserDatas>(request.downloadHandler.text);

    /// <summary>The logic uses Newtonsoft Json package</summary>
    private void SetUserDatasGameplay(UnityWebRequest request)
    {
        Debug.Log(request.downloadHandler.text);
        UserDatasGameplay userDatas = GetUserDatasGameplay(request);
        Entity_Player.Instance.UserDatas.userDatasGameplay = userDatas;
    }

    private UserDatasGameplay GetUserDatasGameplay(UnityWebRequest request) => JsonConvert.DeserializeObject<UserDatasGameplay>(request.downloadHandler.text);

    private void GoToTitleScreenHandler()
    {
        SetInteractability(false);
        _timerDelayedGotoTitleScreen.StartTimer();
    }

    private void SwitchActiveCue(TMPSceneBasedLocalizable cue)
    {
        if (_activeCue == cue) { return; }
        if (_activeCue != null) { _activeCue.gameObject.SetActive(false); }
        _activeCue = cue;
        if (_activeCue != null) { _activeCue.gameObject.SetActive(true); }
    }
}
