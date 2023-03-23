using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class BfaApp
{
    public string appName;
    public string applicationId;
    public string restApiKey;
}

public class BackFourApps : MonoBehaviour
{
    // Shorthands for recurring string calls used in request headers
    public static readonly string appIDS = "X-Parse-Application-Id";
    public static readonly string restAPIKey = "X-Parse-REST-API-Key";
    public static readonly string revocSession = "X-Parse-Revocable-Session";
    public static readonly string contentType = "Content-Type";
    public static readonly string appJson = "application/json";

    // urls
    public static readonly string urlUsers = "https://parseapi.back4app.com/users/";
    public static readonly string urlLogin = "https://parseapi.back4app.com/login/";
    public static readonly string urlClasses = "https://parseapi.back4app.com/classes/";

    // classes
    public static BfaApp ZombieSurvivor = new BfaApp()
    {
        appName = "ZombieSurvivor",
        applicationId = "MhFz3yEuNXtKsZBjcf5RsYgWD8QMigWUgDa6zikZ",
        restApiKey = "o7oRBhPREvO3jXMzFgSlkk8VlkTdRygegBY09xiB"
    };


/*    public static async Task<UserDatas> GetUserDatas(UserDatas playerDatas)
    {
        string url = $"{urlUsers}{playerDatas.objectId}";
        using (var request = new UnityWebRequest(url, "GET"))
        {
            request.SetRequestHeader(appIDS, applicationId);
            request.SetRequestHeader(restAPIKey, restApiKey);

            request.downloadHandler = new DownloadHandlerBuffer();

            request.SendWebRequest();
            while (!request.isDone) { await Task.Yield(); }

            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogWarning("ERROR: " + request.error);
#endif
                return default;
            }

            // Using Newtonsoft Json
            // Debug.Log(JsonConvert.DeserializeObject<UserDatas>(request.downloadHandler.text));
            return JsonConvert.DeserializeObject<UserDatas>(request.downloadHandler.text);
        }
    }*/
}
