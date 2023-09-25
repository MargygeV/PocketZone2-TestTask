using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class AssetInventory : ScriptableObject
{
    public static event UnityAction InventoryChanged;

    public int AmountInventoryCell => _amountInventoryCell;
    public AssetItem[] Items => _items;
    public int[] Amounts => _amounts;

    [SerializeField] private int _amountInventoryCell;
    [SerializeField] private AssetItem[] _items;
    [SerializeField] private int[] _amounts;

    public void AddStackableItem(DroppedItem droppedItem)
    {
        AssetItem newItem = droppedItem.Item;
        int amountNewItem = droppedItem.Amount;

        int cellID = FindCellByItemNameForStack(newItem.Name);

        if(cellID >= 0)
        {
            StackCheck(droppedItem, newItem, cellID, amountNewItem);
        }
    }

    private void StackCheck(DroppedItem droppedItem, AssetItem item, int cellID, int amount)
    {
        int sum = _amounts[cellID] + amount;

        if(sum <= item.MaxAmountInStack)
        {
            AddingItem(item, cellID, amount);
            Destroy(droppedItem.gameObject);
        }
        else
        {
            int differenceToAdd = item.MaxAmountInStack - _amounts[cellID];            
            AddingItem(item, cellID, differenceToAdd);

            int differenceToDrop = sum - item.MaxAmountInStack;
            droppedItem.RecalculateAmount(differenceToDrop);
            AddStackableItem(droppedItem);     
        }
    }

    private void AddingItem(AssetItem item, int cellID, int amount)
    {
        _items[cellID] = item;
        _amounts[cellID] += amount;

        InventoryChanged?.Invoke();
    }

    public void DeleteItem(int cellID, int amount)
    {  
        if(cellID < 0) return;
        
        _amounts[cellID] -= amount;
        if(_amounts[cellID] == 0)
            _items[cellID] = null;

        InventoryChanged?.Invoke();
    }

    private int FindCellByItemNameForStack(string itemName)
    {
        for(int i = 0; i < _items.Length; i++)
        {
            if(_items[i] == null) continue;

            if(_items[i].Name == itemName && _amounts[i] < _items[i].MaxAmountInStack)
                    return i;
        }

        return FindEmptyCell();
    }

    private int FindEmptyCell()
    {
        for(int i = 0; i < _items.Length; i++)
        {
            if(_items[i] == null)
                return i;
        }

        return -1;
    }

    public int FindCellByItemName(string itemName)
    {
        for(int i = 0; i < _items.Length; i++)
        {
            if(_items[i] == null) continue;
            
            if(_items[i].Name == itemName)
                    return i;
        }

        return -1;
    }

    public int ItemsCounterByName(string itemName)
    {
        int counter = 0;

        for(int i = 0; i < _items.Length; i++)
        {
            if(_items[i] == null) continue;
            
            if(_items[i].Name == itemName)
                    counter += _amounts[i];  
        }

        return counter;
    }
}
