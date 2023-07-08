using UnityEngine;

[System.Serializable]
public class SceneData
{
    [SerializeField] private string _name;
    [SerializeField] private int _buildIndex;

    public string Name => _name;

    public int BuildIndex => _buildIndex;
}