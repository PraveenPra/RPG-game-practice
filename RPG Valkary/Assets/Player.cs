using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

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

    [Header("Collision info")]
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundCheckDistance;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private float WallCheckDistance;
    [SerializeField] private LayerMask whereIsGround;

    [Header("Flip info")]
    public int facingDir = 1;
    private bool facingRight = true;

    [Header("Dash info")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.4f;
    public float dashDir;
    [SerializeField]private float dashUsageTime;
    [SerializeField]private float dashCooldown = 3f;

    private void Awake()
    {
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
        primaryAttackState = new PlayerPrimaryAttackState(this,stateMachine,"Attack");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        //because we wont use monobehaviours in the PlayerState & stateMachine classes, we need to pass the update from the play because it has the monobehaviour.
        // Less monobehavior in the game, the better



        stateMachine.currentState.Update();

        CheckForDashInput();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {   //for movement
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(GroundCheck.position, Vector2.down, GroundCheckDistance, whereIsGround);

    public bool IsWallDetected() => Physics2D.Raycast(WallCheck.position, Vector2.right * facingDir, WallCheckDistance, whereIsGround);


    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController(float _x)
    {
        //here x can be used while move, jump etc.. any state needing flip on x
        if (_x < 0 && facingRight)
            Flip();
        else if (_x > 0 && !facingRight)
            Flip();
    }


    void CheckForDashInput()
    {
        if(IsWallDetected())
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

    private void OnDrawGizmos()
    {

        //draw a line from the player(GroundCheck) position to whatever distance(groundcheckdist) value till it touches the ground slightly.Here the line goes down from player to gnd from(0,0) to (0,0-1) = downwards line
        Gizmos.DrawLine(GroundCheck.position, new Vector3(GroundCheck.position.x, GroundCheck.position.y - GroundCheckDistance));

        //Here the line goes down from player to rightside from(0,0) to (0 + 1,0) = rightside line. Just like a graph, draw which side the detection should happen 
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + WallCheckDistance, WallCheck.position.y));
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
