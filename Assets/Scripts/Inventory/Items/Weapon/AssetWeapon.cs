using UnityEngine;

[CreateAssetMenu]
public class AssetWeapon : ScriptableObject
{
    public AssetItem AmmunitionUsed => _ammunitionUsed;
    
    [SerializeField] private AssetItem _ammunitionUsed;
}
