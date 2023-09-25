using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private InventoryPanel _inventoryPanel;

    private void OnEnable()
    {
        _inventoryButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _inventoryButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _inventoryPanel.ChangeEnabled();
    }
}