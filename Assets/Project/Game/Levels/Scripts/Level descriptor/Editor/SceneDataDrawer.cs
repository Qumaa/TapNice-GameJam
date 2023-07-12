using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneData))]
internal class SceneDataDrawer : PropertyDrawer
{
    private const string _NAME_PROP = "_name";
    private const string _BUILD_INDEX_PROP = "_buildIndex";

    private const int _GAP = 6;
    private const int _ID_FIELD_WIDTH = 20;
    private const string _LABEL_TEXT = "Scene";
    private const string _SCENES_MISSING = "Scenes missing";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var nameProp = property.FindPropertyRelative(_NAME_PROP);
        var idProp = property.FindPropertyRelative(_BUILD_INDEX_PROP);

        GetPropsRects(position, label, out var idRect, out var nameRect);

        if (Draw(idProp, nameProp, nameRect, idRect))
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.EndProperty();
    }

    private bool Draw(SerializedProperty idProp, SerializedProperty nameProp, Rect nameRect, Rect idRect)
    {
        var options = EditorBuildSettings.scenes
            .Where(x => x.enabled)
            .Select(x => AssetDatabase.LoadAssetAtPath<SceneAsset>(x.path).name)
            .ToArray();
        var selected = idProp.intValue;
        var idLabel = selected.ToString();

        var disabled = options.Length == 0;
        
        if (disabled)
        {
            options = new[] {_SCENES_MISSING};
            selected = 0;
            idLabel = "-";
        }
        
        // todo: notify when scene is missing

        EditorGUI.BeginChangeCheck();
        
        EditorGUI.BeginDisabledGroup(disabled);
        
        selected = EditorGUI.Popup(nameRect, selected, options);
        EditorGUI.LabelField(idRect, idLabel);
        
        EditorGUI.EndDisabledGroup();
        
        if (!EditorGUI.EndChangeCheck()) 
            return false;
        
        nameProp.stringValue = options[selected];
        idProp.intValue = selected;

        return true;
    }

    private void GetPropsRects(Rect position, GUIContent label, out Rect idRect, out Rect nameRect)
    {
        label.text = _LABEL_TEXT;
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var halfGap = _GAP / 2;
        idRect = new Rect(position.x, position.y, _ID_FIELD_WIDTH - halfGap, position.height);
        nameRect = new Rect(position.x + _ID_FIELD_WIDTH, position.y, position.width - _ID_FIELD_WIDTH - halfGap,
            position.height);

        EditorGUI.indentLevel = indent;
    }
}