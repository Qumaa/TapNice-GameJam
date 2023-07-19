using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

public class InfoLabelElement : VisualElement
{
    private const string _INFO_LABEL_STYLE_CLASS = "info-label";
    private const string _NAME_ELEMENT = "label";
    private const string _INFO_ELEMENT = "info";

    private Label _label, _info;

    public new class UxmlFactory : UxmlFactory<InfoLabelElement, UxmlTraits> {}

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        private UxmlStringAttributeDescription _label = new() {name = "label-text", defaultValue = "Label"};
        private UxmlStringAttributeDescription _info = new() {name = "info-text", defaultValue = "-"};

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var element = ve as InfoLabelElement;
            
            element!.labelText = _label.GetValueFromBag(bag, cc);

            element!.infoText = _info.GetValueFromBag(bag, cc);
        }
    }

    public string labelText
    {
        get => _label.text;
        set => _label.text = value;
    }

    public string infoText
    {
        get => _info.text;
        set => _info.text = value;
    }

    public InfoLabelElement()
    {
        var document = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(SelectorAssetPaths._INFOLABEL_ELEMENT);
        var infoLabel = document.CloneTree();
        
        Add(infoLabel);
        infoLabel.AddToClassList(_INFO_LABEL_STYLE_CLASS);
        _label = infoLabel.Q<Label>(_NAME_ELEMENT);
        _info = infoLabel.Q<Label>(_INFO_ELEMENT);
    }
}