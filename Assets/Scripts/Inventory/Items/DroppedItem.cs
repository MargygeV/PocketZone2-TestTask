using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public AssetItem Item => _item;
    public int Amount => _amount;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AssetItem _item;
    [SerializeField] private int _amount = 1;

    public void InitItem(AssetItem item, int amount)
    {
        _item = item;
        _spriteRenderer.sprite = item.Sprite;
        _amount = amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            player.Inventory.AddStackableItem(this);
        }
    }

    public void RecalculateAmount(int amount)
    {
        _amount = amount;
    }
}
