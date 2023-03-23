using Newtonsoft.Json;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UserLoginController : MonoBehaviour
{
    [Header("Logins")]
    [SerializeField] private TMP_InputField inputFieldEmail;
    [SerializeField] private TMP_InputField inputFieldPassword;

    [Header("Visual cue")]
    [SerializeField] private float cueVisibleTime = 3;
    [SerializeField] private TMPLocalizable cueLocalization;
    private SequentialTimer _timerVerifyEmailCue;

    private void Awake()
    {
        _timerVerifyEmailCue = new(cueVisibleTime, () =>
        {
            cueLocalization.gameObject.SetActive(false);
            _timerVerifyEmailCue.Reset(isPaused: true);
            SceneLoadManager.Instance.GoToTitleScreen();
        });
        _timerVerifyEmailCue.Reset(true);
    }

    private void Update()
    {
        _timerVerifyEmailCue.OnUpdateTime();
    }

    public void SignUp() => StartCoroutine(SignUpIE());
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
                yield break;
            }

            LogIn();
        }
    }

    public void LogIn() => StartCoroutine(LogInIE());
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
                Debug.LogWarning("ERROR: " + request.error);
                yield break;
            }

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
        if (!Entity_Player.Instance.UserDatas.emailVerified)
        {
            cueLocalization.gameObject.SetActive(true);
            _timerVerifyEmailCue.StartTimer();
        }
        else
        {
            SceneLoadManager.Instance.GoToTitleScreen();
        }
    }
}
