using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
  

    #region States

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    #endregion

    [Header("Move info")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

   


    [Header("Dash info")]
    public float dashSpeed = 30f;
    public float dashDuration = 0.1f;
    public float dashDir;
    [SerializeField] private float dashUsageTime;
    [SerializeField] private float dashCooldown = 2f;


    public bool isBusy { get; private set; }//use when u want to stop any state from interupting this state 

    public Vector2[] attackMovements;//hops while attacking


    protected override void Awake()
    {
        base.Awake();
        //because these classes are not monbehaviour im using new keyword to create an instance
        stateMachine = new PlayerStateMachine();

        //the idle, move etc names should be same as bool parameters u create in the animator window
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
      base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {

        base.Update();
        //because we wont use monobehaviours in the PlayerState & stateMachine classes, we need to pass the update from the play because it has the monobehaviour.
        // Less monobehavior in the game, the better



        stateMachine.currentState.Update();

        CheckForDashInput();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

  

   


    void CheckForDashInput()
    {
        if (IsWallDetected())
            return;//dont dash when on wall sliding or on ground but touching wall

        dashUsageTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTime < 0)
        {
            //start the dash ability cooldown counter
            dashUsageTime = dashCooldown;

            //u want to dash on x input direction - dash while moving
            dashDir = Input.GetAxisRaw("Horizontal");

            //dash when idle - no xinput/movement
            if (dashDir == 0)
            {
                dashDir = facingDir;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
