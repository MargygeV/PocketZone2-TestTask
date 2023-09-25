using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour, IPointerClickHandler
{
    public static InventoryPanel Singleton;
    public bool DeleteItemsMod => _deleteItemsMod;

    [SerializeField] private Transform _cellsContainer;
    [SerializeField] private int _amountInventoryCell;
    [SerializeField] private InventoryCell[] _cells;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private DeleteItemPopUp _popUp;
    [SerializeField] private Button _deleteItemsModButton;
    [SerializeField] private Image _deleteButtonImage;

    [SerializeField] private AssetInventory _assetInventory;

    private AssetItem[] _items => _assetInventory.Items;
    private int[] _amounts => _assetInventory.Amounts;

    private bool _deleteItemsMod;

    private void Start()
    {
        _cells = new InventoryCell[_items.Length];
        _deleteButtonImage = _deleteItemsModButton.GetComponent<Image>();
        InitCells();
        gameObject.SetActive(false);  
    }

    private void OnEnable()
    {
        AssetInventory.InventoryChanged += RenderCells;
        _deleteItemsModButton.onClick.AddListener(ChangeDeleteItemsMod);
    }

    private void OnDisable()
    {
        AssetInventory.InventoryChanged -= RenderCells;
        _deleteItemsModButton.onClick.RemoveListener(ChangeDeleteItemsMod);

        _deleteItemsMod = false;
    }

    private void InitCells()
    {
        for(int i = 0; i < _items.Length; i++)
        {
            var newCell = Instantiate(_cellPrefab, _cellsContainer);
            if(newCell.TryGetComponent<InventoryCell>(out InventoryCell inventoryCell))
            {
                _cells[i] = inventoryCell;
                inventoryCell.Init(i, this);
                inventoryCell.Clear();
            }
        }
    }

    private void RenderCells()
    {
        for(int i = 0; i < _items.Length; i++)
        {
            if(_items[i] != null)
                _cells[i].Render(_items[i], _amounts[i]);
            else
                _cells[i].Clear();
        }
    }

    public void ChangeEnabled()
    {
        var enabled = gameObject.activeSelf;

        if(!enabled)
            RenderCells();

        gameObject.SetActive(!enabled);
    }

    public void ChangeDeleteItemsMod()
    {
        _deleteItemsMod = !_deleteItemsMod;

        if(_deleteItemsMod)
            _deleteButtonImage.color = Color.red;
        else
            _deleteButtonImage.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!_deleteItemsMod) return;

        if(eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out InventoryCell cell))
            if(_items[cell.CellID] != null)
                _popUp.InitPanel(_items[cell.CellID], _amounts[cell.CellID], cell.CellID);
    }
}
