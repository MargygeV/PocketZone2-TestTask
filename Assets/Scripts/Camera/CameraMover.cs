using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void FixedUpdate()
    {
        if(_playerTransform != null)
            Move();
    }

    private void Move()
    {
        transform.localPosition = new Vector3(_playerTransform.position.x, _playerTransform.position.y, -1f);
    }
}
