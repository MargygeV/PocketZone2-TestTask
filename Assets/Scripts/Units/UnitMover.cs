using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] protected GameObject _armature;
    [SerializeField] private SpriteRenderer _weaponSprite;

    protected Rigidbody2D Rigidbody;
    protected Vector2 MoveDirection;
    private Vector3 _armatureFlipScale = new Vector3(-1, 1, 1);

    protected virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        Move(MoveDirection);
        ArmatureFlip();
    }

    protected void Move(Vector2 moveDirection)
    {
        Rigidbody.MovePosition(Rigidbody.position + moveDirection * _movementSpeed / 100);
    }

    protected  void ArmatureFlip()
    {
        float directionX = MoveDirection.x;

        if(directionX > 0)
        {
            _armature.transform.localScale = Vector3.one;
            WeaponFlip(false);

        }
        else if(directionX < 0)
        {
            _armature.transform.localScale = _armatureFlipScale;
            WeaponFlip(true);
        }
    }

    private void WeaponFlip(bool newState)
    {
        if(_weaponSprite != null)
            _weaponSprite.flipY = newState;
    }
}
