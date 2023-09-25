using UnityEngine;

public class Bullet : PoolInstance
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _damage;

    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.velocity = transform.up * _movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.ApplyDamage(_damage);

        gameObject.SetActive(false);
    }
}
