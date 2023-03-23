using Newtonsoft.Json;
using System.Collections;
using System.Text;
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
    [SerializeField] private TMPLocalizable localizableCueEmail;
    [SerializeField] private TMPLocalizablePair localizableCueLogin;
    [SerializeField] private TMPLocalizablePair localizableCueSignup;
    private GameObject _activeCue;
    private SequentialTimer _timerDelayedGotoTitleScreen;

    private void Awake()
    {
        _timerDelayedGotoTitleScreen = new(cueVisibleTime, () =>
        {
            SwitchActiveCue(null);
            _timerDelayedGotoTitleScreen.Reset(isPaused: true);
            SceneLoadManager.Instance.GoToTitleScreen();
            localizableCueEmail.gameObject.SetActive(false);
        });
        _timerDelayedGotoTitleScreen.Reset(true);
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

            SwitchActiveCue(localizableCueSignup.gameObject);

            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogWarning("ERROR: " + request.error + " | Email might already exist or is missing an @Foo");
#endif
                localizableCueSignup.SetPair(localizableCueSignup.SecondaryPair);
                localizableCueSignup.LocalizeText();
                yield break;
            }

            localizableCueSignup.SetPair(localizableCueSignup.PrimaryPair);
            localizableCueSignup.LocalizeText();
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

            SwitchActiveCue(localizableCueLogin.gameObject);

            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogWarning("ERROR: " + request.error);
#endif
                localizableCueLogin.SetPair(localizableCueLogin.SecondaryPair);
                localizableCueLogin.LocalizeText();
                yield break;
            }

            localizableCueLogin.SetPair(localizableCueLogin.PrimaryPair);
            localizableCueLogin.LocalizeText();

            SetUserDatas(request);
            GoToTitleScreenHandler();
        }
    }

    /// <summary>The logic uses Newtonsoft Json package</summary>
    private void SetUserDatas(UnityWebRequest request)
    {
        UserDatas userDatas = JsonConvert.DeserializeObject<UserDatas>(request.downloadHandler.text);
        Entity_Player.Instance.UserDatas = userDatas;
    }

    private void GoToTitleScreenHandler()
    {
        SetInteractability(false);
        _timerDelayedGotoTitleScreen.StartTimer();
        if (!Entity_Player.Instance.UserDatas.emailVerified)
        {
            localizableCueEmail.gameObject.SetActive(true);
        }
    }

    private void SwitchActiveCue(GameObject cue)
    {
        if (_activeCue != null) { _activeCue.SetActive(false); }
        _activeCue = cue;
        if (_activeCue != null) { _activeCue.SetActive(true); }
    }
}
