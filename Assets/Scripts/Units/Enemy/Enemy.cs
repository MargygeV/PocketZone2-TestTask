using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private AssetItem[] _dropList;
    [SerializeField] private int[] _amounts;
    [SerializeField] private GameObject _dropItemPrefab;

    protected override void Die()
    {
        DropItems();
        base.Die();
    }

    private void DropItems()
    {
        for(int i = 0; i < _dropList.Length; i++)
        {
            var newObject = Instantiate(_dropItemPrefab, transform.position, Quaternion.identity);
            if(newObject.TryGetComponent<DroppedItem>(out DroppedItem droppedItem))
                droppedItem.InitItem(_dropList[i], _amounts[i]);
        }
    }
}
