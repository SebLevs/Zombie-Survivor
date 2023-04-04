using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PromoCodeManager : MonoBehaviour
{
    private static Dictionary<string, string> _promoCodes;
    private TMP_InputField _inputField;

    private void OnEnable()
    {
        _inputField = GetComponentInChildren<TMP_InputField>();
        _promoCodes = new Dictionary<string, string>();
        StartCoroutine(GetPromoCodes());
    }

    private IEnumerator GetPromoCodes()
    {
        string uri = "https://parseapi.back4app.com/classes/PromoCodes/?where={\"Used\":false}";

        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            var jObject = JObject.Parse(request.downloadHandler.text);
            var results = jObject["results"].ToArray();
            foreach (var result in results)
            {
                var jObjects = JObject.Parse(result.ToString());
                var code = jObjects["Code"].ToString();
                var objectID = jObjects["objectId"].ToString();
                _promoCodes.Add(code, objectID);
            }
        }
    }

    private IEnumerator UpdatePromoCode(string objectID)
    {
        var json = $"{{\"Used\": true}}";
        using (var request = UnityWebRequest.Put(BackFourApps.urlPromoCodes + objectID, json))
        {
            request.SetRequestHeader(BackFourApps.appIDS, BackFourApps.ZombieSurvivor.applicationId);
            request.SetRequestHeader(BackFourApps.restAPIKey, BackFourApps.ZombieSurvivor.restApiKey);
            request.SetRequestHeader(BackFourApps.contentType, BackFourApps.appJson);
            yield return request.SendWebRequest();
        }
    }

    public void CheckCode()
    {
        var input = _inputField.text.ToUpper();
        if (_promoCodes.ContainsKey(input))
        {
            switch (input.ToCharArray().First())
            {
                case 'C':
                {
                    Debug.Log("Gives Currency");
                    break;
                }
                case 'P':
                {
                    Debug.Log("Gives Perma Stats");
                    break;
                }
                case 'T':
                {
                    Debug.Log("Gives Temp Stats");
                    break;
                }
            }

            StartCoroutine(UpdatePromoCode(_promoCodes[input]));
        }
        else
        {
            Debug.Log("Invalid Code");
        }
        _inputField.text = string.Empty;
        _inputField.ActivateInputField();

    }
}