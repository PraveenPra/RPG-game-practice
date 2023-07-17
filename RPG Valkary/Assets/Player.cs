using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb {get; private set;}
    #endregion

    #region States
    
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    #endregion

   [Header("Move info")]
   public float moveSpeed = 7f;
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        //the idle, move etc names should be same as bool parameters u create in the animator window
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
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
    }
public void SetVelocity(float _xVelocity,float _yVelocity)
{
   rb.velocity = new Vector2(_xVelocity,_yVelocity);
}
}
