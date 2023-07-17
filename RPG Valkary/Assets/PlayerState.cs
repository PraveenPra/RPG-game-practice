using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
Debug.Log("I enter"+ animBoolName);
    }

    public virtual void Update()
    {
        //Because there is no update function here due to not monobehaviour, it get the update function from player who is using the state machine
Debug.Log("I in"+ animBoolName);
    }


    public virtual void Exit()
    {
Debug.Log("I exit"+ animBoolName);
    }


}
