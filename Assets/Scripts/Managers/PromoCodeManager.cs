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
                    int temp = Random.Range(0, 2);
                    if (temp == 1)
                    {
                        Entity_Player.Instance.permanentStats.permanentBigGold += 1;
                    }
                    else
                    {
                        Entity_Player.Instance.permanentStats.permanentSmallGold += 10;
                    }
                    break;
                }
                case 'P':
                {
                    Debug.Log("Gives Perma Stats");
                    GivePermaStats();
                    Entity_Player.Instance.UpdateBaseStats();
                    break;
                }
                case 'T':
                {
                    Debug.Log("Gives nice message and Temp Stats");
                    GivePermaStats();
                    Debug.Log(NiceMessage());
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

    private void GivePermaStats()
    {
        CommandInvoker commandInvoker = CommandPromptManager.Instance.playerCommandInvoker;
        int temp = Random.Range(0, commandInvoker.ChestPowerUpDic.Count);
        (CommandType type, ICommand command) = commandInvoker.ChestPowerUpDic.ElementAt(temp);
        string name = type.ToString().Replace("_", " ");
        commandInvoker.DoCommand(command);
        Entity_Player.Instance.RefreshPlayerStats();
    }

    private string NiceMessage()
    {
        List<string> niceMessages = new();
        niceMessages.Add("You are pretty");
        niceMessages.Add("Dont give up");
        niceMessages.Add("CROCODILE!!!");
        niceMessages.Add("Do what makes you happy");
        niceMessages.Add("You are enough, just be you");
        niceMessages.Add("Believe in yourself, you can do it");
        niceMessages.Add("Love wins, always choose kindness");
        niceMessages.Add("Life is short, make it count");
        niceMessages.Add("Dream big, take action now");
        niceMessages.Add("Smile often, spread joy around");
        niceMessages.Add("You are loved, never forget that");
        niceMessages.Add("Be the change you wish for");
        niceMessages.Add("Choose happiness, it's contagious");
        niceMessages.Add("Chase your passions, live your purpose");

        int temp = Random.Range(0, niceMessages.Count);
        return niceMessages[temp];
    }
}