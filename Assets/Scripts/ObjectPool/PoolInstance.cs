using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PoolInstance : InstanceLifeCycle
{
    protected IObjectPool<GameObject> _pool;
    public void SetPool(IObjectPool<GameObject> pool) => _pool = pool;

    private void OnDisable()
    {
        if(_pool != null)
        {
            _pool.Release(gameObject);
        }
    }
}
