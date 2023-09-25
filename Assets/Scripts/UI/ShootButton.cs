using UnityEngine;
using UnityEngine.UI;

public class ShootButton : MonoBehaviour
{
    [SerializeField] private Button _shootingButton;
    [SerializeField] private PlayerShooting _playerShooting;

    private void OnEnable()
    {
        _shootingButton.onClick.AddListener(OnShootButtonClick);
    }

    private void OnDisable()
    {
        _shootingButton.onClick.RemoveListener(OnShootButtonClick);
    }

    private void FixedUpdate()
    {
        _shootingButton.interactable = _playerShooting.CanShoot;
    }

    private void OnShootButtonClick()
    {
        if(_playerShooting == null) return;
            _playerShooting.Shoot();
    }
}
