using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(FindTarget))]
public class PlayerShooting : ObjectPool
{
    public bool CanShoot => _canShoot;

    [SerializeField] private Transform _targetHandlerTransform;
    [SerializeField] private float _offsetRotation;
    [SerializeField] private float _viewRadius = 1;

    private PlayerMover _playerMover;
    private Player _player;
    private AssetInventory _inventory;
    private FindTarget _findTarget;
    private Vector2 _lookingDirection => _playerMover.LookDirection;
    
    private int _amountCurrentAmmunition;
    private bool _canShoot => _findTarget.FindVisibleTargets(transform.position, _viewRadius) != null;

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerMover = GetComponent<PlayerMover>();
        _findTarget = GetComponent<FindTarget>();
        _inventory = _player.Inventory;
    }

    private void FixedUpdate()
    {
        CheckPossibilityShooter();

        if(_lookingDirection != Vector2.zero)
        {
            float rotationZ = Mathf.Atan2(_lookingDirection.y, _lookingDirection.x) * Mathf.Rad2Deg;
            _targetHandlerTransform.rotation = Quaternion.Euler(0f, 0f, rotationZ + _offsetRotation);
        }
    }

    private void CheckPossibilityShooter()
    {
        var enemyInRadius = _findTarget.FindVisibleTargets(transform.position, _viewRadius);
    }

    public void Shoot()
    {
        if(!_canShoot) return;

        GetAmmountCurrentAmmunition();

        if(_amountCurrentAmmunition > 0)
        {
            _pool.Get();
            _inventory.DeleteItem(_inventory.FindCellByItemName(_player.CurrentWeapon.AmmunitionUsed.Name), 1);
        }
    }

    private void GetAmmountCurrentAmmunition()
    {
        if(_player.CurrentWeapon != null)
            _amountCurrentAmmunition = _inventory.ItemsCounterByName(_player.CurrentWeapon.AmmunitionUsed.Name);
    }
}
