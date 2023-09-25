using System.Collections;
using UnityEngine;

public class EnemyAttackOnRadius : FindTarget
{
    public Transform Target => _target;
    public float DistanceToTarget => _distanceToTarget;
    public Vector2 DirectionToTarget => _directionToTarget;
    public float ChaseDistance => _chaseDistance;

    [SerializeField] private float _damage;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _chaseDistance;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _damageCircleRadius;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackAnimationDuration;
    [SerializeField] private float _afterAttackDelay;

    [SerializeField] private bool _debugRadiusFindTarget;

    private Transform _target;
    private float _distanceToTarget;
    private Vector2 _directionToTarget;
    private EnemyMover _mover;
    private Vector2 _firePosition;

    private bool _canAttack = true;

    private void Start()
    {
        _mover = GetComponent<EnemyMover>();
    }

    private void OnDisable()
    {
        StopCoroutine(AttackStart());
    }

    private void FixedUpdate()
    {
        if(_target != null)
        {
            _directionToTarget = (_target.position - transform.position).normalized;
            CheckDistanceToTarget();
        }
        else
            _target = FindVisibleTargets(transform.position, _viewRadius);
    }

    public IEnumerator AttackStart()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackAnimationDuration);

        _firePosition = new Vector2(transform.position.x, transform.position.y) + _directionToTarget * _attackRadius;
        Transform targetInAttackRadius = FindVisibleTargets(_firePosition, _damageCircleRadius);

        if(targetInAttackRadius != null)
            if(targetInAttackRadius.gameObject.TryGetComponent<Player>(out Player player))
                player.ApplyDamage(_damage);

        yield return new WaitForSeconds(_afterAttackDelay);
        _canAttack = true;
    }

    private void CheckDistanceToTarget()
    {
        _distanceToTarget = Vector2.Distance(transform.position, _target.position);

        if(_distanceToTarget <= _attackDistance)
        {
            _mover.ChangeMoveState(false);

            if(_canAttack)
                StartCoroutine(AttackStart());
        }
        else if(!_mover.CanMove)
            _mover.ChangeMoveState(true);
    }

    public void ClearTarget()
    {
        _target = null;
        _distanceToTarget = 0;
    }

    void OnDrawGizmosSelected()
    {
        if(!_debugRadiusFindTarget) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_firePosition, _damageCircleRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseDistance);
    }
}
