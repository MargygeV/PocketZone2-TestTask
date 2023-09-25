using UnityEngine;


public class PlayerMover : UnitMover
{
    public Vector2 LookDirection => MoveDirection;

    private Joystick _joystick;

    protected override void Start()
    {
        base.Start();
        _joystick = Joystick.CurrentJoystick;
    }

    private void Update()
    {
        MoveDirection = new Vector2(_joystick.Horizontal, _joystick.Vertical);
    }
}
