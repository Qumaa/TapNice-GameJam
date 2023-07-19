using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;


public class SceneSelectorWindow : EditorWindow
{
    private const string _LIST_VIEW_NAME = "list-view";
    private const string _NAME_LABEL_NAME = "info-name";
    private const string _PATH_LABEL_NAME = "info-path";
    private const string _CONFIRMATION_BUTTON_NAME = "confirmation-button";

    private SceneAsset[] _availableScenes;
    private Button _confirmButton;
    private InfoLabelElement _nameLabel, _pathLabel;
    private ListView _listView;

    private string[] _selectedPaths;
    private string _nameDefault, _pathDefault;
    public void CreateGUI()
    {
        var document =
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(SelectorAssetPaths._SELECTOR_WINDOW);

        document.CloneTree(rootVisualElement);

        _availableScenes = GetAvailableScenes();
        
        if (_availableScenes.Length == 0)
            return;

        _listView = rootVisualElement.Q<ListView>(_LIST_VIEW_NAME);
        _listView.itemsSource = _availableScenes;
        _listView.makeItem = () => new Label();
        _listView.bindItem = (element, i) => { ((Label) element).text = _availableScenes[i].name; };
        _listView.selectionType = SelectionType.Multiple;
        _listView.onSelectionChange += HandleSelectionChange;

        _nameLabel = rootVisualElement.Q<InfoLabelElement>(_NAME_LABEL_NAME);
        _pathLabel = rootVisualElement.Q<InfoLabelElement>(_PATH_LABEL_NAME);
        _nameDefault = _nameLabel.infoText;
        _pathDefault = _pathLabel.infoText;

        _confirmButton = rootVisualElement.Q<Button>(_CONFIRMATION_BUTTON_NAME);
        _confirmButton.SetEnabled(false);
        _confirmButton.clickable.clicked += HandleConfirmationClick;
    }

    private void OnDestroy()
    {
        _listView.onSelectionChange -= HandleSelectionChange;
        _confirmButton.clickable.clicked -= HandleConfirmationClick;
    }

    private void HandleSelectionChange(IEnumerable<object> obj)
    {
        var selection = obj as object[] ?? obj.ToArray();
        _selectedPaths = new string[selection.Length];
        
        for (int i = 0; i < selection.Length; i++)
        {
            var index = Array.IndexOf(_availableScenes, selection[i] as SceneAsset);
            var selected = _availableScenes[index];
            _nameLabel.infoText = selected.name;
            _pathLabel.infoText = _selectedPaths[i] = AssetDatabase.GetAssetPath(selected);
        }

        _confirmButton.SetEnabled(true);
    }

    private void HandleConfirmationClick()
    {
        var newScenes = new EditorBuildSettingsScene[_selectedPaths.Length];

        for (int i = 0; i < newScenes.Length; i++)
            newScenes[i] = new EditorBuildSettingsScene(_selectedPaths[i], true);

        EditorBuildSettings.scenes = EditorBuildSettings.scenes
            .Concat(newScenes)
            .ToArray();

        _nameLabel.infoText = _nameDefault;
        _pathLabel.infoText = _pathDefault;
        _confirmButton.SetEnabled(false);
        _listView.itemsSource = _availableScenes = GetAvailableScenes();
        _listView.Rebuild();
    }

    private SceneAsset[] GetAvailableScenes()
    {
        var guids = AssetDatabase.FindAssets("t:Scene");

        if (guids.Length == 0)
            return default;

        var names = guids
            .Select(x => AssetDatabase.LoadAssetAtPath<SceneAsset>(AssetDatabase.GUIDToAssetPath(x)))
            .ToArray();

        var except = EditorBuildSettings.scenes
            .Select(x => AssetDatabase.LoadAssetAtPath<SceneAsset>(x.path))
            .ToArray();
        
        return names.Except(except).ToArray();
    }
}