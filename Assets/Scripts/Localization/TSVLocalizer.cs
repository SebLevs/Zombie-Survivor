using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class TSVLocalizer
{
    public static string[] GetHeadersAsString(string fileAndExtension, int startAtColumn)
    {
        string text = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, fileAndExtension));

        string[] lines = text.Split('\n');
        string[] allColumnHeaders = lines[0].Split("\t");
        string[] desiredHeaders = new string[allColumnHeaders.Length - startAtColumn];
        Array.Copy(allColumnHeaders, startAtColumn, desiredHeaders, 0, allColumnHeaders.Length - startAtColumn);
        return desiredHeaders;
    }

    /// <summary>
    /// Automatically ignores the last line break of the Excel sheet (lines.Length - 1)
    /// </summary>
    public static void SetTranslationDatasFromFile(Dictionary<string, ObjectLocalizations> dictionary, string fileAndExtension, int startAtLine, int startAtColumn)
    {
        string text = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, fileAndExtension));

        string[] lines = text.Split('\n');

        for (int line = startAtLine; line < lines.Length - 1; line++)
        {
            string[] columns = lines[line].Split("\t");

            // Set a translation container for the current key value
            ObjectLocalizations objectLocalizations = new();
            for (int languageIndex = startAtColumn + 1; languageIndex < columns.Length; languageIndex++)
            {
                objectLocalizations.Localizations!.Add((Languages)languageIndex, columns[languageIndex]);
            }
            dictionary!.Add(columns[startAtColumn], objectLocalizations);
        }
    }

    /// <summary>
    /// Automatically ignores the last line break of the Excel sheet (lines.Length - 1)
    /// </summary>
    public static void SetTranslationDatasFromFile(Dictionary<string, ObjectLocalizations> dictionary, TextAsset textAsset, int startAtLine, int startAtColumn)
    {
        string text = textAsset.text;

        string[] lines = text.Split('\n');

        for (int line = startAtLine; line < lines.Length - 1; line++)
        {
            string[] columns = lines[line].Split("\t");

            // Set a translation container for the current key value
            ObjectLocalizations objectLocalizations = new();
            for (int languageIndex = startAtColumn + 1; languageIndex < columns.Length; languageIndex++)
            {
                objectLocalizations.Localizations!.Add((Languages)languageIndex, columns[languageIndex]);
            }
            dictionary!.Add(columns[startAtColumn], objectLocalizations);
        }
    }

    public static string GetObjectLocalizationValue(Dictionary<string, ObjectLocalizations> localizations, string key, Languages language)
    {
        if (!localizations.ContainsKey(key)) { return ""; }
        return localizations[key].Localizations[language];
    }
}
