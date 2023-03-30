using Newtonsoft.Json;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class UserLoginController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Logins")]
    [SerializeField] private TMP_InputField inputFieldEmail;
    [SerializeField] private TMP_InputField inputFieldPassword;

    [Header("Visual cue")]
    [SerializeField] private float cueVisibleTime = 3;
    [SerializeField] private TMPLocalizable localizableCueEmailVerification;
    [Space]
    [SerializeField] private TMPLocalizable localizableCueValid;
    [SerializeField] private string keyValidLogin;
    [SerializeField] private string keyValidSignup;
    [Space]
    [SerializeField] private TMPLocalizable localizableCueInvalid;
    [SerializeField] private string keyInvalidEmailCombination;
    private TMPLocalizable _activeCue;
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
        using (var request = new UnityWebRequest(BackFourApps.urlUsers, "POST"))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);
            request.SetRequestHeader(BackFourApps.revocSession, "1");
            request.SetRequestHeader(BackFourApps.contentType, BackFourApps.appJson);

            var data = new { username = inputFieldEmail.text, email = inputFieldEmail.text, password = inputFieldPassword.text };
            var json = JsonConvert.SerializeObject(data);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogWarning("ERROR: " + request.error + " | Email might already exist or is missing an @Foo");
#endif
                SwitchActiveCue(localizableCueInvalid);
                // TODO: Make COR checks here
                //string key = _errorHander.TryLoginHandleError();
                //_activeCue.LocalizeExternalText(key); // TODO: Pass check key value here
                _activeCue.LocalizeExternalText("TEST"); // TODO: Remove when above is implemented
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
                // TODO: Make COR checks here
                //string key = _errorHander.TryLoginHandleError();
                //_activeCue.LocalizeExternalText(key); // TODO: Pass check key value here
                _activeCue.LocalizeExternalText("TEST"); // TODO: Remove when above is implemented
                yield break;
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
    }

    public void GetUsers() { StartCoroutine(GetUsersIE()); }
    public IEnumerator GetUsersIE() // Get User from data base
    {
        yield break;
    }


    public void UpdateUserDatas() { StopAllCoroutines(); StartCoroutine(UpdateUserDatasIE()); }
    public IEnumerator UpdateUserDatasIE() // Get User from data base
    {
        yield break;
    }

    /// <summary>The logic uses Newtonsoft Json package</summary>
    private void SetUserDatas(UnityWebRequest request)
    {
        //UserDatas userDatas = JsonConvert.DeserializeObject<UserDatas>(request.downloadHandler.text);
        UserDatas userDatas = GetUserDatas(request);
        Entity_Player.Instance.UserDatas = userDatas;
    }

    private UserDatas GetUserDatas(UnityWebRequest request) => JsonConvert.DeserializeObject<UserDatas>(request.downloadHandler.text);

    private void GoToTitleScreenHandler()
    {
        SetInteractability(false);
        _timerDelayedGotoTitleScreen.StartTimer();
    }

    private void SwitchActiveCue(TMPLocalizable cue)
    {
        if (_activeCue == cue) { return; }
        if (_activeCue != null) { _activeCue.gameObject.SetActive(false); }
        _activeCue = cue;
        if (_activeCue != null) { _activeCue.gameObject.SetActive(true); }
    }
}
