using Project.Game;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelDescriptor))]
internal class SceneDescriptorInspector : Editor
{
    private const string _SCENE_DATA_PROP = "_targetScene";
    private const string _SCRIPT_PROP = "m_Script";
    private const string _NO_SCENES_MESSAGE = "There are no scenes added to build settings. Unable to select a scene.";
    private const string _OPEN_BUILD_SETTINGS = "Open build settings";
    private const string _SELECT_A_SCENE = "Select a scene to add to build settings";
    private const int _PROPERTIES_AND_BUTTONS_SPACE = 10;

    public override void OnInspectorGUI()
    {
        DrawScriptProperty();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty(_SCENE_DATA_PROP));

        if (EditorBuildSettings.scenes.Length == 0)
        {
            EditorGUILayout.HelpBox(_NO_SCENES_MESSAGE, MessageType.Warning, true);
            EditorGUI.BeginDisabledGroup(true);
        }

        DrawPropertiesExcluding(serializedObject, _SCENE_DATA_PROP, _SCRIPT_PROP);
        EditorGUI.EndDisabledGroup();
        
        GUILayout.Space(_PROPERTIES_AND_BUTTONS_SPACE);

        if (GUILayout.Button(_OPEN_BUILD_SETTINGS)) 
            EditorWindow.CreateWindow<BuildPlayerWindow>();

        if (GUILayout.Button(_SELECT_A_SCENE)) 
            EditorWindow.CreateWindow<SceneSelectorWindow>();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawScriptProperty()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(_SCRIPT_PROP));
        EditorGUI.EndDisabledGroup();
    }
}