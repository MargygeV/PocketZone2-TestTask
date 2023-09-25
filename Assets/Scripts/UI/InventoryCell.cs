using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryCell : MonoBehaviour
{
    public int CellID => _cellID;

    [SerializeField] private Image _imageField;
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private GameObject _textBG;

    private int _cellID;
    private InventoryPanel _inventoryPanel;

    public void Render(AssetItem item, int amount)
    {
        _imageField.sprite = item.Sprite;
        _imageField.enabled = true;
        ChangeCount(amount);
    }

    public void Init(int newID, InventoryPanel inventoryPanel)
    {
        _cellID = newID;
        _inventoryPanel = inventoryPanel;
    }

    public void Clear()
    {
        _imageField.sprite = null;
        _imageField.enabled = false;
        ChangeCount(0);
    }

    private void ChangeCount(int amount)
    {
        if(amount > 1)
        {
            _textBG.SetActive(true);
            _textField.text = "" + amount;
        }
        else
            _textBG.SetActive(false);
    }   
}
