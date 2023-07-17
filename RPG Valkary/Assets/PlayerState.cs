using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
protected Rigidbody2D rb; //just for easy ref everywhere without using player.rb instead use rb
    private string animBoolName;
    protected float xInput;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        // Debug.Log("I enter"+ animBoolName);
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void Update()
    {
        //Because there is no update function here due to not monobehaviour, it get the update function from player who is using the state machine
        xInput = Input.GetAxisRaw("Horizontal");
    }


    public virtual void Exit()
    {
        // Debug.Log("I exit"+ animBoolName);
        player.anim.SetBool(animBoolName, false);
    }


}
