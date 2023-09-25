using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeleteItemPopUp : MonoBehaviour
{
    [SerializeField] private Button _deleteItemButton;
    [SerializeField] private Image _itemIconField;
    [SerializeField] private TMP_Text _textField;

    [SerializeField] private AssetInventory _inventory;

    private int _cellID;
    private int _amount;

    private void Start()
    {
        gameObject.SetActive(false);  
    }

    private void OnEnable()
    {
        _deleteItemButton.onClick.AddListener(OnDeleteButtonClick);
    }

    private void OnDisable()
    {
        _deleteItemButton.onClick.RemoveListener(OnDeleteButtonClick);
    }

    public void InitPanel(AssetItem item, int amount, int cellID)
    {
        _cellID = cellID;
        _amount = amount;
        _itemIconField.sprite = item.Sprite;
        _textField.text = "Удалить " + item.Name + " x" + amount + "?";
        
        gameObject.SetActive(true);
    }

    private void OnDeleteButtonClick()
    {
        _inventory.DeleteItem(_cellID, _amount);
        gameObject.SetActive(false);
    }
}