using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SceneDataHandler: MonoBehaviour
{
    private const string sceneFolder = "Assets/Scenes/";
    private static string[] sceneFolders = new[] { sceneFolder };
    private const string sceneDataFolder = "Assets/Scriptables/SceneDatas";
    private static string[] sceneDataFolders = new[] { sceneDataFolder };

    [MenuItem("Tool/SceneData/Create default folder paths")]
    private static void TryCreateDefaultFolders()
    {
        string[] sceneFolders = sceneFolder.Split('/');
        TryCreateFolders(sceneFolders);

        string[] assetFolders = sceneDataFolder.Split('/');
        TryCreateFolders(assetFolders);
    }

    private static void TryCreateFolders(string[] folders)
    {
        string folderPath = "";
        foreach (string assetFolder in folders)
        {
            if (!Directory.Exists(Path.Combine(folderPath, assetFolder)))
            {
                AssetDatabase.CreateFolder(folderPath, assetFolder);
            }
            folderPath = Path.Combine(folderPath, assetFolder);
        }
    }

    [MenuItem("Tool/SceneData/Create Scene Assets With folder hierarchy")]
    private static void CreateSceneAssetsWithFolderHierarchy()
    {
        string[] assetFolders = sceneDataFolder.Split('/');
        TryCreateFolders(assetFolders);

        string defaultAssetDataPath = Path.Combine(sceneDataFolder.Split("/"));
        foreach (var sceneAsset in GetSceneAssets())
        {
            string createAssetAtPath = defaultAssetDataPath;
            SceneData sceneData = ScriptableObject.CreateInstance<SceneData>();
            sceneData.SceneAsset = sceneAsset;

            string assetFolderName = Path.GetDirectoryName(AssetDatabase.GetAssetPath(sceneAsset.GetInstanceID()));
            string[] cleanedFolderPath = assetFolderName.Substring(sceneFolder.Length - 1).Split("\\");

            foreach (string folder in cleanedFolderPath)
            {
                if (!Directory.Exists(Path.Combine(createAssetAtPath, folder)))
                {
                    AssetDatabase.CreateFolder(createAssetAtPath, folder);
                }
                createAssetAtPath = Path.Combine(createAssetAtPath, folder);
            }

            createAssetAtPath = Path.Combine(createAssetAtPath, sceneData.SceneAsset.name + ".asset");
            TryCreateSceneDataAsset(createAssetAtPath, sceneData);
        }
    }

    private static List<SceneAsset> GetSceneAssets()
    {
        string[] sceneAssetsGuid = AssetDatabase.FindAssets("t:SceneAsset", sceneFolders);
        List<SceneAsset> sceneAssets = new();
        foreach (var sceneGuid in sceneAssetsGuid)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            sceneAssets.Add(AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath));
        }
        return sceneAssets;
    }

    private static void TryCreateSceneDataAsset(string specificAssetDataPath, SceneData sceneData)
    {
        SceneData existCheck = AssetDatabase.LoadAssetAtPath<SceneData>(specificAssetDataPath);
        if (!existCheck || existCheck.SceneAsset != sceneData.SceneAsset)
        {
            AssetDatabase.CreateAsset(sceneData, specificAssetDataPath);
        }
    }


    [MenuItem("Tool/SceneData/Update scene asset names")]
    private static void UpdateScriptableNameFromSceneAsset()
    {
        string[] sceneDatasGuid = AssetDatabase.FindAssets("t:SceneData", sceneDataFolders);
        foreach (var sceneDataGuid in sceneDatasGuid)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(sceneDataGuid);
            SceneData sceneData = AssetDatabase.LoadAssetAtPath<SceneData>(assetPath);
            if (sceneData.name == sceneData.SceneAsset.name)
            { continue; }
            AssetDatabase.RenameAsset(assetPath, sceneData.SceneAsset.name);
        }

        AssetDatabase.SaveAssets();
    }
}