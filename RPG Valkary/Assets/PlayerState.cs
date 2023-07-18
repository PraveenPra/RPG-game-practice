using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    #region Declarations
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;


    protected float xInput;
    protected Rigidbody2D rb; //just for easy ref everywhere without using player.rb instead use rb
    protected float stateTimer;
    #endregion

    #region Constructor
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }
    #endregion

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void Update()
    {
        //Because there is no update function here due to not monobehaviour, it get the update function from player who is using the state machine
        xInput = Input.GetAxisRaw("Horizontal");

        stateTimer -= Time.deltaTime;
    }


    public virtual void Exit()
    {
        // Debug.Log("I exit"+ animBoolName);
        player.anim.SetBool(animBoolName, false);
    }


}
