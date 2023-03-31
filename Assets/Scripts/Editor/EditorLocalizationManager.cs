#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR


[CustomEditor(typeof(LocalizationManager))]
public class EditorLocalizationManager : Editor
{
    private LocalizationManager _localizationManager;
    private Languages _lastLanguage;

    private void OnEnable()
    {
        _localizationManager = target as LocalizationManager;
        _lastLanguage = _localizationManager.Language;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        EndChangeCheckBehaviours();

        serializedObject.ApplyModifiedProperties();
    }

    private void EndChangeCheckBehaviours()
    {
        if (EditorGUI.EndChangeCheck())
        {
            if (Application.isPlaying)
            {
                NotifyIlocalizationListeners();
            }
        }
    }

    private void NotifyIlocalizationListeners()
    {
        if (_lastLanguage != _localizationManager.Language)
        {
            _lastLanguage = _localizationManager.Language;
            _localizationManager.NotifyLocalizationListeners();
        }
    }
}
#endif
