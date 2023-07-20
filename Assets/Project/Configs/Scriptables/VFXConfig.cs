using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(menuName = SOData.MenuPath.VFX_CONFIG, fileName = SOData.FileName.VFX_CONFIG)]
    public class VFXConfig : ScriptableObject
    {
        [field: SerializeField] public float DensityPerUnit { get; private set; }
    }
}