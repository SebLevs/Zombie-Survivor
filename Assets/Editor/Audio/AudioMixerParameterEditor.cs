using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioMixerParameter))]
public class AudioMixerParameterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(
            "Note\n" +
            "The parameter name is the asset name", MessageType.Info);

        base.OnInspectorGUI();
    }
}
