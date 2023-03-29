using UnityEngine;

public class SceneData : ScriptableObject
{
#if UNITY_EDITOR // Required to prevent crash in build
    public UnityEditor.SceneAsset SceneAsset;
#endif
}
