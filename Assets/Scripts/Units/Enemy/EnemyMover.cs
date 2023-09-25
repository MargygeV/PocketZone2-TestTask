using UnityEngine;

[RequireComponent(typeof(EnemyAttackOnRadius))]
public class EnemyMover : UnitMover
{
    public bool CanMove => _canMove;

    private float _distanceToTarget => _attack.DistanceToTarget;
    private float _chaseDistance => _attack.ChaseDistance;

    private Transform _target => _attack.Target;
    private EnemyAttackOnRadius _attack;

    private bool _canMove = true;

    protected override void Start()
    {
        
        base.Start();
        _attack = GetComponent<EnemyAttackOnRadius>();
    }

    protected override void FixedUpdate()
    {
        if(_target != null)
        {
            if(_distanceToTarget <= _chaseDistance)
            {
                if(_canMove)
                {
                    MoveDirection = _attack.DirectionToTarget;
                    Move(MoveDirection);
                }
            }
            else
                _attack.ClearTarget();
        }
        else
            Move(Vector2.zero);

        ArmatureFlip();
    }

    public void ChangeMoveState(bool newState)
    {
        _canMove = newState;
    }
}
