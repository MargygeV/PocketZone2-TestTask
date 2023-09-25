using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private float _maxHealth;

    private Slider _slider;
    private float _currentHealth;

    protected virtual void Start()
    {
        _canvas = _canvas.transform.GetChild(0).gameObject;
        _slider = _canvas.GetComponent<Slider>();
        
        ChangeHealth(_maxHealth);
    }

    public virtual void ApplyDamage(float damage)
    {
        ChangeHealth(_currentHealth - damage);

        if(_currentHealth <= 0)
            Die();
    }

    protected virtual void ChangeHealth(float newHealth)
    {
        _currentHealth = newHealth;
        _slider.value = _currentHealth / _maxHealth;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
