using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(0,0);//stop all movements when u enetr idle st from anywhere
    }

    public override void Update()
    {
        base.Update();

        if(xInput != 0 && !player.isBusy)
        stateMachine.ChangeState(player.moveState);

        if(xInput == player.facingDir && player.IsWallDetected())
        return; //when player moves to the wall he should not play any move animation, just stop the execution of movement
    }


    public override void Exit()
    {
        base.Exit();
    }
}

// To copy paste basic code from, when u create new state
// public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
//     {

//     }

//     public override void Enter()
//     {
//         base.Enter();
//     }

//     public override void Update()
//     {
//         base.Update();
//     }

//     public override void Exit()
//     {
//         base.Exit();
//     }