using UnityEngine;

[CreateAssetMenu]
public class AssetItem : ScriptableObject
{
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public int MaxAmountInStack => _maxAmountInStack;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _maxAmountInStack = 1;
}
