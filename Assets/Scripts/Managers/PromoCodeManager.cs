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

    [Header("Localization Visual Cue")]
    [SerializeField] private TMPLocalizablePair promoLocalizablePair;
    [SerializeField] private TMPSceneBasedLocalizable promoLocalizablePromoCode;
    [SerializeField] private TMPSceneBasedLocalizable promoLocalizableStat;
    [Header("Visual cue keys")]
    [SerializeField] private string keySmallGold;
    [SerializeField] private string keyBigGold;
    [SerializeField] private string keyPermaStat;
    [SerializeField] private string keyTempStat;


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
        promoLocalizablePair.gameObject.SetActive(true);
        var input = _inputField.text.ToUpper();
        if (_promoCodes.ContainsKey(input))
        {
            StartCoroutine(UpdatePromoCode(_promoCodes[input]));

            promoLocalizablePair.SetPair(promoLocalizablePair.PrimaryPair);
            promoLocalizablePromoCode.gameObject.SetActive(true);
            promoLocalizableStat.gameObject.SetActive(true);
            switch (input.ToCharArray().First())
            {
                case 'C':
                {
                    int temp = Random.Range(0, 2);
                    if (temp == 1)
                    {
                        Entity_Player.Instance.permanentStats.permanentBigGold += 1;
                        promoLocalizablePromoCode.LocalizeExternalText(keySmallGold);
                    }
                    else
                    {
                        Entity_Player.Instance.permanentStats.permanentSmallGold += 10;
                        promoLocalizablePromoCode.LocalizeExternalText(keyBigGold);
                    }
                    break;
                }
                case 'P':
                {
                    string keyStat = CommandPromptManager.Instance.GetLocalizationKeyForCommand(GivePermaStats());
                    Entity_Player.Instance.UpdateBaseStats();
                    promoLocalizablePromoCode.LocalizeExternalText(keyPermaStat);
                    promoLocalizableStat.LocalizeExternalText(keyStat);
                    break;
                }
                case 'T':
                {
                    string keyStat = CommandPromptManager.Instance.GetLocalizationKeyForCommand(GivePermaStats());
                    promoLocalizablePromoCode.LocalizeExternalText(keyTempStat);
                    promoLocalizableStat.LocalizeExternalText(keyStat);
                    break;
                }
            }
        }
        else
        {
            promoLocalizablePair.SetPair(promoLocalizablePair.SecondaryPair);
            promoLocalizablePromoCode.gameObject.SetActive(false);
            promoLocalizableStat.gameObject.SetActive(false);
        }
        promoLocalizablePair.LocalizeText();
        _inputField.text = string.Empty;
        _inputField.ActivateInputField();
    }

    private CommandType GivePermaStats()
    {
        CommandInvoker commandInvoker = CommandPromptManager.Instance.playerCommandInvoker;
        int temp = Random.Range(0, commandInvoker.ChestPowerUpDic.Count);
        (CommandType type, ICommand command) = commandInvoker.ChestPowerUpDic.ElementAt(temp);
        string name = type.ToString().Replace("_", " ");
        commandInvoker.DoCommand(command);
        Entity_Player.Instance.RefreshPlayerStats();
        return type;
    }
}