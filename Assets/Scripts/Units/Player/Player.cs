using UnityEngine;

public class Player : Unit
{
    public AssetInventory Inventory => _inventory;
    public AssetWeapon CurrentWeapon => _currentWeapon;

    [SerializeField] private AssetInventory _inventory;
    [SerializeField] private AssetWeapon _currentWeapon;
}
