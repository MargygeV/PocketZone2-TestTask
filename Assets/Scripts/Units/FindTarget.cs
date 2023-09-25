using System;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    [SerializeField] private int _quantityTargets = 1;

    [SerializeField] protected LayerMask TargetMask;
    [SerializeField] private LayerMask _obstacleMask;

    private Collider2D[] _colliders;

    private void Awake()
    {
        _colliders = new Collider2D[_quantityTargets];
    }

    public Transform FindVisibleTargets(Vector3 center, float viewRadius)
    {
        Array.Clear(_colliders, 0, _colliders.Length);
        Physics2D.OverlapCircleNonAlloc(center, viewRadius, _colliders, TargetMask);

        for(int i = 0; i < _colliders.Length; i++)
        {
            if(_colliders[i] == null) continue;
            Transform target = _colliders[i].transform;
            Vector2 directionToTarget = (target.position - center).normalized;
            
            float distanceToTarget = Vector2.Distance(center, target.position);

            if(!Physics2D.Raycast(center, directionToTarget, distanceToTarget, _obstacleMask))
                return target;
        }
        
        return null;
    }
}
